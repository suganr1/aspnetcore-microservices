import { Injectable } from '@angular/core';

import { EnvService } from './env.service';
import { Observable, of } from 'rxjs';

import { MatDialog } from '@angular/material/dialog';

import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})

export class CommonService {
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  apiUrl: string = "";

  constructor(
    public env: EnvService,
    public dialogService: MatDialog,
    private _snackBar: MatSnackBar) {
    this.apiUrl = this.env.apiUrl;
  }

  getExchangeTypes() {
    return [{ "Id": 1, "Value": "BSE" }, { "Id": 2, "Value": "NSE" }];
  }

  dialogOpen(component: any, data: any) {
    return this.dialogService.open(component, data);
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 3000,
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      panelClass: ['mat-toolbar', 'mat-primary']
    });
  }

  handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.log('URL:' + this.apiUrl);
      console.log('ENV:' + this.env.environmentName);
      // TODO: send the error to remote logging infrastructure
      console.log(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      console.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
