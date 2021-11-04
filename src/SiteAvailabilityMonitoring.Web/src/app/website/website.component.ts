import { Component, OnInit } from '@angular/core';
import { ApiService } from '../config/api.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from "@angular/material/table";

export interface Website {
  id: number
  address: string
  available: boolean
  status_code: number
}

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {

  apiService: ApiService;

  addWebsiteForm!: FormGroup;

  displayedColumns: Array<string> = ['id', 'address', 'available', 'status_code', 'delete'];
  dataSource!: MatTableDataSource<Website>;

  websites!: Website[];

  constructor(private formBuilder: FormBuilder, apiService: ApiService) {
    this.apiService = apiService;
    this.dataSource = new MatTableDataSource<Website>();
  }

  ngOnInit(): void {
    this.addWebsiteForm = this.formBuilder.group({
      address: ['']
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

  addWebsites() {
    const add = { addresses: [this.controls().address.value] };
    this.apiService
      .addWebsites(add)
      .subscribe(websites => {
        this.websites.push(websites[0]);
        this.setWebsitesIntoTable();
        this.controls().address.reset();
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

  checkWebsites() {
    this.apiService
      .checkWebsites()
      .subscribe();
  }

  setWebsitesIntoTable() {
    this.dataSource.data = this.websites
  }

  controls() {
    return this.addWebsiteForm.controls;
  }

  removeWebsite(id: number) {
    const removeIndex = this.websites.findIndex(item => item.id === id);
    this.websites.splice(removeIndex, 1);
  }
}
