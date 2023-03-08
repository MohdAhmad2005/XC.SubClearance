import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class AesEncryption {

  private AesKey: string = environment.encryption.AesKey;
  private AesIV: string = environment.encryption.AesIV;

  constructor() { }

  encrypt(value) {
    var key = CryptoJS.enc.Utf8.parse(this.AesKey);
    var iv = CryptoJS.enc.Utf8.parse(this.AesIV);
    var encrypted = '';
    if (!!value) {
      encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(value.toString()), key,
        {
          keySize: 128 / 8,
          iv: iv,
          mode: CryptoJS.mode.CBC,
          padding: CryptoJS.pad.Pkcs7
        });
    }

    return encrypted.toString();
  }

  //The get method is use for decrypt the value.
  decrypt(encryptedValue) {
    var key = CryptoJS.enc.Utf8.parse(this.AesKey);
    var iv = CryptoJS.enc.Utf8.parse(this.AesIV);

    var decrypted = null;
    if (!!encryptedValue) {
      decrypted = CryptoJS.AES.decrypt(encryptedValue, key, {
        keySize: 128 / 8,
        iv: iv,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
      }).toString(CryptoJS.enc.Utf8);
    }


    return decrypted;
  }

}
