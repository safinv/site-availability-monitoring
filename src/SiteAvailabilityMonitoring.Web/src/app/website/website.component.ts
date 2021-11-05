import { Component, OnInit } from '@angular/core';
import { ApiService } from '../config/api.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from "@angular/material/table";

export interface Website {
  id: number
  address: string
  available: boolean
  status_code: number
}

export interface AddWebsite {
  address: string;
}

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {

  apiService: ApiService;

  displayedColumns: Array<string> = ['id', 'address', 'available', 'status_code', 'delete'];
  dataSource!: MatTableDataSource<Website>;

  addWebsiteForm!: FormGroup;

  websites: Website[];
  addWebsite: AddWebsite;
  errorMessage: string;

  errorAlert: string;
  successAlert: string;

  constructor(private fb: FormBuilder, apiService: ApiService) {
    this.apiService = apiService;
    this.dataSource = new MatTableDataSource<Website>();
  }

  ngOnInit(): void {
    this.addWebsite = { address: "" };
    this.addWebsiteForm = new FormGroup({
      newUrl: new FormControl(this.addWebsite.address, [
        Validators.required,
        Validators.minLength(1)
      ])
    });

    this.showWebsites();
  }

  showWebsites() {
    this.apiService.getWebsites()
      .subscribe((data: Array<Website>) => {
        this.websites = data;
        this.setWebsitesIntoTable();
      });
  }

  insertWebsite() {
    this.apiService
      .insertWebsite(this.addWebsite)
      .pipe()
      .subscribe(
        website => {
          this.websites.push(website);
          this.setWebsitesIntoTable();
          this.addWebsiteForm.controls.newUrl.reset();
        },
        error => {
          this.errorMessage = error.error;
        });
  }

  deleteWebsite(id: number) {
    this.apiService
      .deletewebsite(id)
      .subscribe(() => {
        this.removeWebsite(id);
        this.setWebsitesIntoTable();
      });
  }

  setWebsitesIntoTable() {
    this.dataSource.data = this.websites
  }

  removeWebsite(id: number) {
    const removeIndex = this.websites.findIndex(item => item.id === id);
    this.websites.splice(removeIndex, 1);
  }

  closeForm() {
    this.errorMessage = '';
    this.successAlert = '';
  }

  editWebsite(id: number, event: any) {
    this.apiService
      .updateWebsite(id, event.target.value)
      .pipe()
      .subscribe(
        website => { 
          this.successAlert = "Success update";
        },
        error => {
          this.errorMessage = error.error;
        });
  }
}
