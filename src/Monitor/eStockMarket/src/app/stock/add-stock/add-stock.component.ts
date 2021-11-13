import { Component, Inject, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { StockService } from "./../../_services/stock.service";

@Component({
  selector: 'app-add-stock',
  templateUrl: './add-stock.component.html',
  styleUrls: ['./add-stock.component.scss']
})

export class AddStockComponent implements OnInit {
  myForm!: FormGroup;
  companyCode: string = '';

  constructor(
    @Inject(MAT_DIALOG_DATA) data: any,
    private dialogRef: MatDialogRef<AddStockComponent>,
    private fb: FormBuilder, private stockService: StockService) {
    this.companyCode = data.companyCode;
  }

  ngOnInit(): void {
    if (this.companyCode) {
      this.reactiveForm();
    }
  }

  reactiveForm() {
    this.myForm = this.fb.group({
      companyCode: [{ value: this.companyCode, disabled: true }, [Validators.required, Validators.minLength(3), Validators.maxLength(8), Validators.pattern('^[a-zA-Z0-9_]*$')]],
      price: [null, [Validators.required, Validators.maxLength(8), Validators.min(0), Validators.max(99999), Validators.pattern(/^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$/)]]
    });
  }

  submitForm() {
    if (this.myForm.valid) {
      this.stockService.addStock(this.myForm.getRawValue()).subscribe(data => {
        this.dialogRef.close(data);
      }, err => {
        console.log(err);
      });
    }
  }

  closeDialog() {
    this.dialogRef.close(null);
  }
}
