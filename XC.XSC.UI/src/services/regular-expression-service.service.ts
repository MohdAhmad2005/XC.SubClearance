import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
/*
 * Service: Regular Expression Service
 * Uses: This service is used to store all the regular expressions that we want to use in the product.
 *
 * */
export class RegularExpressionServiceService {

  constructor() { }

  public Password: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*]).{8,}$/;
  public caseNumber: RegExp = /^([a-zA-Z]+)+\g?([0-9]+)$/;
  public Phone: RegExp = /^[0-9]*$/;///^[+]?\d+$/;//
  public specialCharactersAndNumeric: RegExp = /^[^a-zA-Z]+$/;
  public NumbersBetweenZeroToHundered: RegExp = /^[0-9]$|^[1-9][0-9]$|^(100)$/;
}
