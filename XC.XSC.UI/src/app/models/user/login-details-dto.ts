import { UserInfoDto } from "./user-info-dto";

export interface LoginDetailsDto {
    userName: string;
    password: string;
}

export interface ForgotPasswordDto {
  newPassword: string;
  confirmPassword: string;
  userId: string;
}

export interface LogoutDto {
    accessToken: string;
    refreshToken: string;
}

export interface RecoverEmailDto {
  Email: string;
}

export interface ResetPasswordDto {
  userName: string;
  oldPassword: string;
  newPassword: string;
  confirmPassword: string;
}
