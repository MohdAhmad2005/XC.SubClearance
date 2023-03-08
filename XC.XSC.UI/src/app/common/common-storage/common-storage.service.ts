import { Injectable } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { AccountService } from 'src/app/Services/account/account.service';
import { JwtService } from 'src/app/Services/auth/jwt-service.service';
import { AesEncryption } from '../../../services/aes-encryption-service.service';
import { UserCommonStorage } from './user-common-storage';

@Injectable({
  providedIn: 'root'
})
export class CommonStorageService {

  private key: any;
    
  constructor(private _cryptoService: AesEncryption,
    private _keycloakService: KeycloakService,
    private _accountService: AccountService,
    private _jwtService :JwtService) {
      
      this._keycloakService.getToken().then((token) => {  
        const tokenPayload = this._jwtService.GetTokenDecoded(token);
        this.key = this._cryptoService.encrypt(tokenPayload['preferred_username']);
      });      
  }

  public setCommonStorage(userCommonStorage: UserCommonStorage): void {
    localStorage.removeItem(this.key)
    localStorage.setItem(this.key, this._cryptoService.encrypt(JSON.stringify(userCommonStorage)));
  }

  public getCommonStorage(): any {
    const commonStorage = this._cryptoService.decrypt(localStorage.getItem(this.key));
    if (commonStorage) {
      return JSON.parse(commonStorage);
    }
    return {};
  }

  public removeCommonStorage(): void {
    localStorage.removeItem(this.key);
  }

}
