using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Helpers.Utilities;
using MPMSRS.Models.Entities;
using MPMSRS.Models.VM;
using MPMSRS.Services.Interfaces;
using MPMSRS.Services.Interfaces.Auth;
using MPMSRS.Services.Interfaces.ICaller;
using MPMSRS.Services.Repositories;
using MPMSRS.Services.Repositories.Auth;
using Newtonsoft.Json;

namespace MPMSRS.Controllers.Auth
{
    [Route("api/login")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        public const string SessionOTPMPM = "_Otp";
        public const string SessionEmail = "_Email";
        public const string SessionPass = "_Pass";
        public const string SessionUser = "_User";

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private IMPMClient _client;
        private IVPSClient _vpsClient;
        private FileServices _fileService;
        private readonly Authenticator _services;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly MyEmailSenderRepository _mailsend;

        public LoginApiController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, Authenticator services, MyEmailSenderRepository mailsend, IMPMClient mpmClient, IVPSClient vpsClient, RefreshTokenValidator refreshTokenValidator, IRefreshTokenRepository refreshTokenRepository, FileServices fs)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _services = services;
            _mailsend = mailsend;
            _client = mpmClient;
            _vpsClient = vpsClient;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
            _fileService = fs;
        }

        [HttpPost("auth-login", Name = "AuthLogin")]
        public async Task<IActionResult> AuthLogin([FromBody] LoginDto model)
        {
            try
            {
                var thisDate = DateTime.Now.Date;
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }

                //var byPass = true;
                var response = await _client.GetMPMOrange(model);
                if (response.Body.isUserValidResponse.isUserValidReturn.ToLower() == "false")
                //if(byPass == false)
                {
                    _logger.LogError("User object sent from client wrong username or password.");
                    return BadRequest("User wrong username or password");
                }
                else
                {
                    //check user adadi db atau tidak?
                    var conUser = model.UserId.Trim().Substring(1);
                    var user = await _repository.User.GetUserByUsernameLogin(conUser);
                    if (user == null)
                    {
                        //get username ke mpm
                        string res = model.UserId.Trim().Substring(0, 1);
                        if (res.Contains('0'))
                        {
                            res = model.UserId.Trim().Substring(1);
                        }
                        else
                        {
                            res = model.UserId.Trim();
                        }
                        var getToken = await _vpsClient.GetTokenAuthorization();
                        if (!string.IsNullOrEmpty(getToken.key))
                        {
                            var vpsClient = await _vpsClient.GetUserFromClient(res,getToken.key);
                            if (vpsClient.data.Count() > 0)
                            {
                                UserMPMData tmpUser = new UserMPMData()
                                {
                                    employeeid = vpsClient.data.Select(Q => Q.employeeid).FirstOrDefault(),
                                    companyid = vpsClient.data.Select(Q => Q.companyid).FirstOrDefault(), // H0000
                                    worklocation = vpsClient.data.Select(Q => Q.worklocation).FirstOrDefault(), //sby
                                    display_name = vpsClient.data.Select(Q => Q.display_name).FirstOrDefault(), //dummy
                                    jobtitle = vpsClient.data.Select(Q => Q.jobtitle).FirstOrDefault(), //STAFF
                                    picture = vpsClient.data.Select(Q => Q.picture).FirstOrDefault(), //kosong
                                    division = vpsClient.data.Select(Q => Q.division).FirstOrDefault(), // STAFF DEALER
                                    departement = vpsClient.data.Select(Q => Q.departement).FirstOrDefault(), //DEALER
                                    phone = vpsClient.data.Select(Q => Q.phone).FirstOrDefault(), //kosong
                                    email = vpsClient.data.Select(Q => Q.email).FirstOrDefault() //kosong
                                };

                                //AuthenticationForCreationDto userNewCache = new AuthenticationForCreationDto();
                                //userNewCache.Username = model.UserId;
                                //userNewCache.Password = model.Password;
                                //userNewCache.UserData = JsonConvert.SerializeObject(tmpUser);

                                //var authEntity = _mapper.Map<Authentications>(userNewCache);
                                //_repository.Authentication.CreateAuthentication(authEntity);
                                //_repository.Save();

                                //var result = JsonConvert.DeserializeObject<UserMPMData>(authenticationEntity.UserData);
                                var url = "";
                                var mime = "";
                                var blobId = Guid.Empty;

                                if (!string.IsNullOrEmpty(tmpUser.picture))
                                {
                                    url = tmpUser.picture;
                                    mime = tmpUser.picture.Split('.')[1];
                                    blobId = await _fileService.InsertBlob(url, mime);
                                }

                                UserForCreationDto newUser = new UserForCreationDto();

                                newUser.CompanyId = tmpUser.companyid;
                                newUser.Username = tmpUser.employeeid;
                                newUser.Password = model.Password;
                                newUser.WorkLocation = tmpUser.worklocation;
                                newUser.DisplayName = tmpUser.display_name;
                                newUser.Department = tmpUser.departement;
                                newUser.Phone = tmpUser.phone;
                                newUser.Email = tmpUser.email;
                                newUser.InternalTitle = tmpUser.jobtitle;
                                newUser.CreatedAt = thisDate;
                                newUser.CreatedBy = "SYSTEM";
                                newUser.UpdatedAt = thisDate;
                                newUser.UpdatedBy = "SYSTEM";

                                var clientLogin = _mapper.Map<UserDto>(newUser);

                                var divCheck = await _repository.Division.GetDivisionByUsername(tmpUser.division.Trim());

                                if (divCheck == null)
                                {
                                    DivisionCreationForDto dDto = new DivisionCreationForDto();
                                    dDto.DivisionName = tmpUser.division.Trim();
                                    dDto.CreatedAt = thisDate;
                                    dDto.CreatedBy = "SYSTEM";
                                    dDto.UpdatedAt = thisDate;
                                    dDto.UpdatedBy = "SYSTEM";

                                    var divisionEntity = _mapper.Map<Divisions>(dDto);
                                    _repository.Division.CreateDivision(divisionEntity);
                                    _repository.Save();
                                    var createdDivision = _mapper.Map<DivisionDto>(divisionEntity);

                                    newUser.DivisionId = createdDivision.DivisionId;
                                    clientLogin.DivisionId = createdDivision.DivisionId;
                                    clientLogin.DivisionName = tmpUser.division.Trim();
                                }
                                else
                                {
                                    newUser.DivisionId = divCheck.DivisionId;
                                    clientLogin.DivisionId = divCheck.DivisionId;
                                    clientLogin.DivisionName = divCheck.DivisionName;
                                }

                                var rolesCheck = await _repository.Role.GetRoleByRoleName("Main Dealer");

                                if (rolesCheck == null)
                                {
                                    RoleForCreationDto rDto = new RoleForCreationDto();
                                    rDto.RoleName = "Main Dealer";
                                    rDto.CreatedAt = thisDate;
                                    rDto.CreatedBy = "SYSTEM";
                                    rDto.UpdatedAt = thisDate;
                                    rDto.UpdatedBy = "SYSTEM";

                                    var roleEntity = _mapper.Map<Roles>(rDto);
                                    _repository.Role.CreateRole(roleEntity);
                                    _repository.Save();
                                    var createdRole = _mapper.Map<RoleDto>(roleEntity);

                                    newUser.RoleId = createdRole.RoleId;
                                    clientLogin.RoleId = createdRole.RoleId;
                                    clientLogin.RoleName = createdRole.RoleName;
                                }
                                else
                                {
                                    newUser.RoleId = rolesCheck.RoleId;
                                    clientLogin.RoleId = rolesCheck.RoleId;
                                    clientLogin.RoleName = "Main Dealer";
                                }

                                if (blobId != Guid.Empty)
                                {
                                    newUser.AttachmentId = blobId;
                                    clientLogin.AttachmentId = blobId;
                                }

                                var userEntity = _mapper.Map<Users>(newUser);
                                _repository.User.CreateUser(userEntity);
                                _repository.Save();
                                var createdUser = _mapper.Map<UserDto>(userEntity);
                                CreatedAtRoute("UserById", new { id = createdUser.EmployeeId }, createdUser);
                                clientLogin.EmployeeId = createdUser.EmployeeId;
                                var tokenUsr = _services.Authenticate(createdUser);

                                UserFcmTokenForCreationDto usrFcm = new UserFcmTokenForCreationDto();
                                usrFcm.EmployeeId = clientLogin.EmployeeId;
                                usrFcm.Token = model.UserFcmToken;
                                usrFcm.CreatedAt = thisDate;
                                usrFcm.CreatedBy = "SYSTEM";
                                usrFcm.UpdatedAt = thisDate;
                                usrFcm.UpdatedBy = "SYSTEM";

                                var userFcmEntity = _mapper.Map<UserFcmTokens>(usrFcm);
                                _repository.UserFcmToken.CreateUserFcmToken(userFcmEntity);
                                _repository.Save();

                                UserMPMLogin resultUser = new UserMPMLogin()
                                {
                                    Users = clientLogin,
                                    Token = tokenUsr.Result
                                };
                                return Ok(resultUser);
                                //return Ok(tmpUser);
                            }
                            else
                            {
                                _logger.LogError("Wrong Parameter");
                                return BadRequest("Wrong Parameter");
                            }
                        }
                        else
                        {
                            _logger.LogError("Token Could not be Generated");
                            return BadRequest("Token Could not be Generated");
                        }
                    }
                    else
                    {
                        UserFcmTokenForUpdateDto updateFcm = new UserFcmTokenForUpdateDto();
                        updateFcm.EmployeeId = user.EmployeeId;
                        updateFcm.Token = model.UserFcmToken;
                        updateFcm.CreatedAt = thisDate;
                        updateFcm.CreatedBy = "SYSTEM";
                        updateFcm.UpdatedAt = thisDate;
                        updateFcm.UpdatedBy = "SYSTEM";

                        var userFcmTokenEntity = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(user.EmployeeId);
                        if (userFcmTokenEntity == null)
                        {
                            _logger.LogError($"User Fcm Token with employee id: {userFcmTokenEntity.EmployeeId}, hasn't been found in db.");
                            return NotFound();
                        }
                        _mapper.Map(updateFcm, userFcmTokenEntity);
                        _repository.UserFcmToken.UpdateUserFcmToken(userFcmTokenEntity);
                        _repository.Save();

                        var userResult = _mapper.Map<UserDto>(user);
                        var tokenUsr = _services.Authenticate(userResult);
                        UserMPMLogin resultUser = new UserMPMLogin()
                        {
                            Users = userResult,
                            Token = tokenUsr.Result
                        };

                        return Ok(resultUser);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, ex);
            }
        }

        [HttpPost("auth-login-dealer", Name = "AuthLoginDealer")]
        public async Task<IActionResult> AuthLoginDealer([FromBody] LoginDealerDto model)
        {
            try
            {
                var thisDate = DateTime.Now.Date;
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }
                var user = await _repository.User.GetUserByUsernameLoginDealer(model.DealerCode);
                if (user == null)
                {
                    var getToken = await _vpsClient.GetTokenAuthorization();
                    if (!string.IsNullOrEmpty(getToken.key))
                    {
                        var vpsClient = await _vpsClient.GetVPSSecretKey(getToken.key, model);
                        if (vpsClient.data.Count() > 0)
                        {
                            var vpsUserDealer = await _vpsClient.GetVPSUserDealer(getToken.key, vpsClient.data[2], model);
                            if (vpsUserDealer.data.Count() > 0)
                            {
                                UserMPMData tmpUser = new UserMPMData()
                                {
                                    employeeid = vpsUserDealer.data[4],
                                    companyid = vpsUserDealer.data[0], // H0000
                                    worklocation = vpsUserDealer.data[6], //sby
                                    display_name = vpsUserDealer.data[1], //dummy
                                    jobtitle = vpsUserDealer.data[3], //STAFF
                                    picture = "", //kosong
                                    division = vpsUserDealer.data[8], // STAFF DEALER
                                    departement = vpsUserDealer.data[5], //DEALER
                                    phone = "", //kosong
                                    email = "" //kosong
                                };

                                UserForCreationDto newUser = new UserForCreationDto();

                                newUser.CompanyId = tmpUser.companyid;
                                newUser.Username = tmpUser.employeeid;
                                newUser.Password = vpsUserDealer.data[2];
                                newUser.WorkLocation = tmpUser.worklocation;
                                newUser.DisplayName = tmpUser.display_name;
                                newUser.Department = tmpUser.departement;
                                newUser.Phone = tmpUser.phone;
                                newUser.Email = tmpUser.email;
                                newUser.InternalTitle = tmpUser.jobtitle;
                                newUser.CreatedAt = thisDate;
                                newUser.CreatedBy = "SYSTEM";
                                newUser.UpdatedAt = thisDate;
                                newUser.UpdatedBy = "SYSTEM";

                                var clientLogin = _mapper.Map<UserDto>(newUser);

                                var divCheck = await _repository.Division.GetDivisionByUsername(tmpUser.division.Trim());

                                if (divCheck == null)
                                {
                                    DivisionCreationForDto dDto = new DivisionCreationForDto();
                                    dDto.DivisionName = tmpUser.division.Trim();
                                    dDto.CreatedAt = thisDate;
                                    dDto.CreatedBy = "SYSTEM";
                                    dDto.UpdatedAt = thisDate;
                                    dDto.UpdatedBy = "SYSTEM";

                                    var divisionEntity = _mapper.Map<Divisions>(dDto);
                                    _repository.Division.CreateDivision(divisionEntity);
                                    _repository.Save();
                                    var createdDivision = _mapper.Map<DivisionDto>(divisionEntity);

                                    newUser.DivisionId = createdDivision.DivisionId;
                                    clientLogin.DivisionId = createdDivision.DivisionId;
                                    clientLogin.DivisionName = tmpUser.division.Trim();
                                }
                                else
                                {
                                    newUser.DivisionId = divCheck.DivisionId;
                                    clientLogin.DivisionId = divCheck.DivisionId;
                                    clientLogin.DivisionName = divCheck.DivisionName;
                                }

                                var rolesCheck = await _repository.Role.GetRoleByRoleName("Dealer");

                                if (rolesCheck == null)
                                {
                                    RoleForCreationDto rDto = new RoleForCreationDto();
                                    rDto.RoleName = "Dealer";
                                    rDto.CreatedAt = thisDate;
                                    rDto.CreatedBy = "SYSTEM";
                                    rDto.UpdatedAt = thisDate;
                                    rDto.UpdatedBy = "SYSTEM";

                                    var roleEntity = _mapper.Map<Roles>(rDto);
                                    _repository.Role.CreateRole(roleEntity);
                                    _repository.Save();
                                    var createdRole = _mapper.Map<RoleDto>(roleEntity);

                                    newUser.RoleId = createdRole.RoleId;
                                    clientLogin.RoleId = createdRole.RoleId;
                                    clientLogin.RoleName = createdRole.RoleName;
                                }
                                else
                                {
                                    newUser.RoleId = rolesCheck.RoleId;
                                    clientLogin.RoleId = rolesCheck.RoleId;
                                    clientLogin.RoleName = "Dealer";
                                }

                                var userEntity = _mapper.Map<Users>(newUser);
                                _repository.User.CreateUser(userEntity);
                                _repository.Save();
                                var createdUser = _mapper.Map<UserDto>(userEntity);
                                CreatedAtRoute("UserById", new { id = createdUser.EmployeeId }, createdUser);
                                clientLogin.EmployeeId = createdUser.EmployeeId;
                                var tokenUsr = _services.Authenticate(createdUser);

                                UserFcmTokenForCreationDto usrFcm = new UserFcmTokenForCreationDto();
                                usrFcm.EmployeeId = clientLogin.EmployeeId;
                                usrFcm.Token = model.UserFcmToken;
                                usrFcm.CreatedAt = thisDate;
                                usrFcm.CreatedBy = "SYSTEM";
                                usrFcm.UpdatedAt = thisDate;
                                usrFcm.UpdatedBy = "SYSTEM";

                                var userFcmEntity = _mapper.Map<UserFcmTokens>(usrFcm);
                                _repository.UserFcmToken.CreateUserFcmToken(userFcmEntity);
                                _repository.Save();

                                UserMPMLogin resultUser = new UserMPMLogin()
                                {
                                    Users = clientLogin,
                                    Token = tokenUsr.Result
                                };
                                return Ok(resultUser);
                            }
                            else
                            {
                                _logger.LogError("Secret Key Invalid");
                                return BadRequest("Secret Key Invalid");
                            }
                        }
                        else
                        {
                            _logger.LogError("Wrong Parameter");
                            return BadRequest("Wrong Parameter");
                        }

                    }
                    else
                    {
                        _logger.LogError("Token Could not be Generated");
                        return BadRequest("Token Could not be Generated");
                    }
                }
                else
                {
                    if(user.Password.Trim() == model.PasswordHash.Trim())
                    {
                        UserFcmTokenForUpdateDto updateFcm = new UserFcmTokenForUpdateDto();
                        updateFcm.EmployeeId = user.EmployeeId;
                        updateFcm.Token = model.UserFcmToken;
                        updateFcm.CreatedAt = thisDate;
                        updateFcm.CreatedBy = "SYSTEM";
                        updateFcm.UpdatedAt = thisDate;
                        updateFcm.UpdatedBy = "SYSTEM";

                        var userFcmTokenEntity = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(user.EmployeeId);
                        if (userFcmTokenEntity == null)
                        {
                            _logger.LogError($"User Fcm Token with employee id: {userFcmTokenEntity.EmployeeId}, hasn't been found in db.");
                            return NotFound();
                        }
                        _mapper.Map(updateFcm, userFcmTokenEntity);
                        _repository.UserFcmToken.UpdateUserFcmToken(userFcmTokenEntity);
                        _repository.Save();

                        var userResult = _mapper.Map<UserDto>(user);
                        var tokenUsr = _services.Authenticate(userResult);
                        UserMPMLogin resultUser = new UserMPMLogin()
                        {
                            Users = userResult,
                            Token = tokenUsr.Result
                        };

                        return Ok(resultUser);
                    }
                    else
                    {
                        _logger.LogError("Wrong Username or Password");
                        return BadRequest("Wrong Username or Password");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, ex);
            }
        }

        [HttpPost("auth-login-admin", Name = "AuthLoginAdmin")]
        public async Task<IActionResult> AuthLoginAdmin([FromBody] LoginDto model)
        {
            try
            {
                var thisDate = DateTime.Now.Date;
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }

                var user = await _repository.User.GetUserByUsernameLogin(model.UserId.Trim());
                if (user == null)
                {
                    _logger.LogError("User object sent from client wrong username or password.");
                    return BadRequest("User wrong username or password");
                }
                else
                {
                    var role = await _repository.Role.GetRoleById(user.RoleId.HasValue ? user.RoleId.Value : Guid.Empty);
                    if(role.RoleName.Trim() == "Admin")
                    {
                        var checkUserFcm = await _repository.UserFcmToken.GetUserFcmTokenByEmployeeId(user.EmployeeId);
                        if (checkUserFcm == null)
                        {
                            UserFcmTokenForCreationDto usrFcm = new UserFcmTokenForCreationDto();
                            usrFcm.EmployeeId = user.EmployeeId;
                            usrFcm.Token = model.UserFcmToken;
                            usrFcm.CreatedAt = thisDate;
                            usrFcm.CreatedBy = "SYSTEM";
                            usrFcm.UpdatedAt = thisDate;
                            usrFcm.UpdatedBy = "SYSTEM";

                            var userFcmEntity = _mapper.Map<UserFcmTokens>(usrFcm);
                            _repository.UserFcmToken.CreateUserFcmToken(userFcmEntity);
                            _repository.Save();
                        }
                        if (user.Username.Trim() == model.UserId && user.Password.Trim() == model.Password)
                        {
                            var userResult = _mapper.Map<UserDto>(user);
                            var tokenUsr = _services.Authenticate(userResult);

                            UserMPMLogin resultUser = new UserMPMLogin()
                            {
                                Users = userResult,
                                Token = tokenUsr.Result
                            };

                            return Ok(resultUser);
                        }
                        else
                        {
                            _logger.LogError("User object sent from client wrong username or password.");
                            return BadRequest("User wrong username or password");
                        }
                    }
                    else
                    {
                        _logger.LogError("User object role sent is not belong to Admin User.");
                        return BadRequest("User object role sent is not belong to Admin User");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, ex);
            }
        }

        [HttpPost("send-otp-user", Name = "SendOTPUser")]
        public async Task<IActionResult> SendOTPUser([FromQuery] Guid employeeId, string emailUser)
        {
            try
            {
                string num = "0123456789";
                int len = num.Length;
                string otp = string.Empty;
                int otpdigit = 4;
                string finalDigit;
                int getIndex;

                for (int i = 0; i < otpdigit; i++)
                {
                    do
                    {
                        getIndex = new Random().Next(0, len);
                        finalDigit = num.ToCharArray()[getIndex].ToString();

                    } while (otp.IndexOf(finalDigit) != -1);
                    otp += finalDigit;
                }

                var userEntity = await _repository.User.GetUserById(employeeId);

                AuthenticationForCreationDto userNewCache = new AuthenticationForCreationDto();
                userNewCache.Username = userEntity.EmployeeId.ToString();
                userNewCache.Otp = otp;
                userNewCache.UserData = JsonConvert.SerializeObject(userEntity);

                var authEntity = _mapper.Map<Authentications>(userNewCache);
                _repository.Authentication.CreateAuthentication(authEntity);
                _repository.Save();

                if (emailUser.Trim() == "devsrsmpm@gmail.com")
                {
                    //_mailsend.SendEmail(emailUser, "MPMSRS-OTP", "<p>WASPADA PENIPUAN. JANGAN BERI KODE OTP INI KE SIAPAPUN BAHKAN PIHAK MPMSRS. BERLAKU 2 Menit. KODE OTP <u>" + otp + "</u> PESAN INI ADALAH PESAN OTOMATIS, MOHON UNTUK TIDAK DIBALAS </p>");
                    _mailsend.SendEmail("devsrsmpm@gmail.com", "MPMSRS-OTP", "<p>WASPADA PENIPUAN. JANGAN BERI KODE OTP INI KE SIAPAPUN BAHKAN PIHAK MPMSRS. BERLAKU 2 Menit. KODE OTP <u>" + otp + "</u> PESAN INI ADALAH PESAN OTOMATIS, MOHON UNTUK TIDAK DIBALAS </p>");
                    return Ok();
                }
                else
                {
                    _logger.LogError("Email Does Not Match With The Current System.");
                    return BadRequest("Email Does Not Match With The Current System.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost("confirm-otp-user", Name = "ConfirmOtpUser")]
        public async Task<IActionResult> ConfirmOTPUser([FromQuery] string OtpFromUser, Guid employeeId, string newPassword, string confirmPassword)
        {
            try
            {
                var thisDate = DateTime.Now.Date;

                var authenticationEntity = await _repository.Authentication.GetAuthenticationByUsername(employeeId.ToString().Trim());

                var OtpSystem = authenticationEntity.Otp;

                if(authenticationEntity != null)
                {
                    if (OtpSystem.Trim() == OtpFromUser.Trim())
                    {
                        if(newPassword.Trim().Equals(confirmPassword.Trim()))
                        {
                            var result = JsonConvert.DeserializeObject<Users>(authenticationEntity.UserData);
                            if (result == null)
                            {
                                _logger.LogError("User object sent from client is null.");
                                return BadRequest("User object is null");
                            }
                            if (!ModelState.IsValid)
                            {
                                _logger.LogError("Invalid user object sent from client.");
                                return BadRequest("Invalid model object");
                            }

                            UserForUpdateDto user = new UserForUpdateDto();
                            user.EmployeeId = result.EmployeeId;
                            user.AttachmentId = result.AttachmentId;
                            user.DivisionId = result.DivisionId;
                            user.RoleId = result.RoleId;
                            user.CompanyId = result.CompanyId;
                            user.Username = result.Username;
                            user.Password = confirmPassword;
                            user.WorkLocation = result.WorkLocation;
                            user.DisplayName = result.DisplayName;
                            user.Department = result.Department;
                            user.Phone = result.Phone;
                            user.Email = result.Email;
                            user.InternalTitle = result.InternalTitle;
                            user.CreatedAt = result.CreatedAt;
                            user.CreatedBy = result.CreatedBy;
                            user.UpdatedBy = result.EmployeeId.ToString();
                            user.UpdatedAt = thisDate;

                            var userEntity = await _repository.User.GetUserById(user.EmployeeId);
                            if (userEntity == null)
                            {
                                _logger.LogError($"Role with id: {user.EmployeeId}, hasn't been found in db.");
                                return NotFound();
                            }
                            _mapper.Map(user, userEntity);
                            _repository.User.UpdateUser(userEntity);
                            var authEntity = await _repository.Authentication.GetAuthenticationByUsername(employeeId.ToString().Trim());
                            if (authEntity != null)
                            {
                                _repository.Authentication.DeleteAuthentication(authEntity);
                            }
                            _repository.Save();
                            return Ok("Password Berhasil di Reset");
                        }
                        else
                        {
                            _logger.LogError("New Password Does Not Match With The Confirm Password.");
                            return BadRequest("New Password Does Not Match With The Confirm Password.");
                        }
                    }
                    else
                    {
                        _logger.LogError("OTP Does Not Match With The Current System.");
                        return BadRequest("OTP Does Not Match With The Current System.");
                    }
                }
                else
                {
                    _logger.LogError("User Does Not Collect Properly by the System.");
                    return BadRequest("User Does Not Collect Properly by the System.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside ConfirmOTPUser action: {ex.Message}");
                return StatusCode(500, ex);
            }
        }

        [HttpPost("auth-refresh", Name = "AuthRefresh")]
        public async Task<IActionResult> AuthRefresh([FromBody] RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestModelState();
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new MyErrorResponse("Invalid refresh token."));
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                return NotFound(new MyErrorResponse("Invalid refresh token."));
            }

            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

            Users user = await _repository.User.GetUserById(refreshTokenDTO.EmployeeId);
            if (user == null)
            {
                return NotFound(new MyErrorResponse("User not found."));
            }
            var createdUser = _mapper.Map<UserDto>(user);
            AuthenticatedUserResponse response = await _services.Authenticate(createdUser);

            return Ok(response);
        }

        [HttpPost("auth-forget-password-main-dealer", Name = "AuthForgetPasswordMainDealer")]
        public async Task<IActionResult> AuthForgotPasswordMainDealer([FromBody] LoginForgetPasswordDto forgetPassword)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequestModelState();
                }
                var response = await _client.MPMResetPassword(forgetPassword.UserId, forgetPassword.Email);
                if (response.Body.resetPasswordResponse.resetPasswordReturn.ToLower().Trim().Equals("email tidak sesuai"))
                {
                    _logger.LogError("User object sent from client wrong username, email or body parts.");
                    return BadRequest("User wrong username, email or body parts.");
                }
                else
                {
                    _logger.LogError("User object password has reset successfully .");
                    return Ok(response.Body.resetPasswordResponse.resetPasswordReturn.ToLower());
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside AuthForgotPasswordMainDealer action: {ex.Message}");
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpDelete("auth-logout", Name = "AuthLogout")]
        public async Task<IActionResult> AuthLogout([FromQuery] string userIdUsername)
        {
            // using header {bearer (token)}
            string rawUserId = HttpContext.User.FindFirstValue("id");
            var authEntity = await _repository.Authentication.GetAuthenticationByUsername(userIdUsername);

            if(authEntity!=null)
            {
                _repository.Authentication.DeleteAuthentication(authEntity);
                _repository.Save();
            }

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepository.DeleteAll(userId);

            return Ok();
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

            return BadRequest(new MyErrorResponse(errorMessages));
        }
    }
}
