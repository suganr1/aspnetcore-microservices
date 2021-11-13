import { Component, OnInit } from '@angular/core';

import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

import { Company } from "./../../model/company.model";

import { CompanyService } from "./../../_services/company.service";

@Component({
  selector: 'app-create-company',
  templateUrl: './create-company.component.html',
  styleUrls: ['./create-company.component.scss']
})

export class CreateCompanyComponent implements OnInit {
  companyList: Company[] = new Array<Company>();

  myForm!: FormGroup;

  exchangeType: any = [
    { id: 1, name: 'BSE' },
    { id: 2, name: 'NSE' }
  ];

  wasFormChanged = false;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<CreateCompanyComponent>,
    public companyService: CompanyService
  ) { }

  ngOnInit(): void {
    this.companyService.getAllCompanies()
      .subscribe(companyList => {
        this.companyList = companyList;
      });
    this.reactiveForm();
  }

  reactiveForm() {
    this.myForm = this.fb.group({
      name: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(60), Validators.pattern('^[a-zA-Z ]*$')]],
      code: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(8), Validators.pattern('^[a-zA-Z0-9_]*$')]],
      ceo: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(25), Validators.pattern('^[a-zA-Z ]*$')]],
      turnOver: [null, [Validators.required, Validators.minLength(9), Validators.maxLength(16), Validators.min(100000000), Validators.max(1000000000000000)]],
      website: [null, [Validators.required, Validators.maxLength(60)]],
      exchangeTypeId: [null, [Validators.required]]
    },
      {
        validators: this.companyAlreadyExists('code')
      }
    );

    this.myForm.valueChanges.subscribe(form => {
      this.myForm.patchValue({
        name: form.name?.toUpperCase()
      }, { emitEvent: false });
      this.myForm.patchValue({
        code: form.code?.toUpperCase()
      }, { emitEvent: false });
      this.myForm.patchValue({
        ceo: form.ceo?.toUpperCase()
      }, { emitEvent: false });
      this.myForm.patchValue({
        website: form.website?.toUpperCase()
      }, { emitEvent: false });
    });
  }

  companyAlreadyExists(code: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control) {
        const codeField = control.get(code);
        const codeFieldValue = codeField?.value;

        if (codeField?.errors && !codeField?.errors.notUnique) {
          return null;
        }

        const alreadyExists = this.companyList?.find(f => f.code === codeFieldValue);
        if (alreadyExists) {
          codeField?.setErrors({ notUnique: true });
          return ({ notUnique: true });
        } else {
          codeField?.setErrors(null);
          return null;
        }
      }
      return null;
    }
  }

  submitForm() {
    if (this.myForm.valid) {
      this.companyService.createCompany(this.myForm.value).subscribe(data => {
        this.dialogRef.close(data);
      }, err => {
        console.log(err);
      });
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  changeTextToUppercase(field: string) {
    this.myForm.controls[field].setValue(this.myForm.controls[field].value.toUpperCase());
  }
}
