import { EnvService } from './env.service';

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { CommonService } from "./../_services/common.service";
import { Company } from "./../model/company.model";

@Injectable({
  providedIn: 'root'
})

export class CompanyService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  apiUrl: string = "";

  constructor(
    public env: EnvService,
    private http: HttpClient,
    private commonService: CommonService) {
      this.apiUrl = this.env.apiUrl;
  }

  getAllCompanies(): Observable<Company[]> {
    //const url = `${this.apiUrl}/api/Company`;
    const url = `${this.apiUrl}/v1.0/market/company/getall`;

    return this.http.get<Company[]>(url)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<Company[]>('Error in getAllCompanies()', []))
      );
  }

  createCompany(company: Company): Observable<any> {
    //const url = `${this.apiUrl}Company/${code}`;
    const url = `${this.apiUrl}/v1.0/market/company/register`;
    return this.http.post(url, company)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<any>(`Error in createCompany() - id=${company.code}`))
      );
  }

  deleteCompany(code: string): Observable<any> {
    //const url = `${this.apiUrl}/Company/${code}`;
    const url = `${this.apiUrl}/v1.0/market/company/delete/${code}`;

    return this.http.delete(url)
      .pipe(
        catchError(this.commonService.handleError<any>(`Error in deleteCompany() - id=${code}`))
      );
  }
}
