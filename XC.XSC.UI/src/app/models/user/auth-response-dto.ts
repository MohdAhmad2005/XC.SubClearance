import { UserInfoDto } from "./user-info-dto";

export interface AuthResponseDto {
    statusCode: number;
    isAuthSuccessful:boolean;
    message: tokenDetails;
    userInfo: UserInfoDto;
    error: string;
    error_description: string;
}

export interface tokenDetails {
    accessToken: string;
    expiresIn: number;
    refreshExpiresIn: number;
    refreshToken: string;
    tokenType: string;
    notBeforePolicy: number;
    sessionState: string;
    scope: string;
}
