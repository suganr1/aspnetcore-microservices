import { EnvService } from './env.service';

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { CommonService } from "./../_services/common.service";

import { Company } from "./../model/company.model";
import { Aggregator } from "./../model/aggregator.model";

@Injectable({
  providedIn: 'root'
})

export class AggregatorService {

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

  getCompanyByCode(code: string): Observable<Aggregator> {
    //const url = `${this.apiUrl}Company/${code}`;
    const url = `${this.apiUrl}/v1.0/market/aggregator/${code}`;

    return this.http.get<Aggregator>(url)
      .pipe(
        tap(_ => { }),
        catchError(this.commonService.handleError<Aggregator>(`Error in getCompanyByCode() - Aggregator - Code=${code}`))
      );
  }
}
