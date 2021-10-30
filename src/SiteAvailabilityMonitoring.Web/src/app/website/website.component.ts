import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../config/config.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export interface Website {
  id: number
  address: string
  status: string
}

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {

  configService: ConfigService;

  addWebsiteForm!: FormGroup;
  websites!: Website[];

  constructor(private formBuilder: FormBuilder, configService: ConfigService) {
    this.configService = configService;
  }

  ngOnInit(): void {
    this.addWebsiteForm = this.formBuilder.group({
      address: ['']
    });
    this.showWebsites();
  }

  showWebsites() {
    this.configService.getWebsites()
      .subscribe((data: Array<Website>) => this.websites = data);
  }

  addWebsites() {
    const add = { addresses: [this.controls().address.value] };
    this.configService
      .addWebsites(add)
      .subscribe(websites => this.websites.push(websites[0]));
  }

  deleteWebsite(id: number) {
    this.configService
      .deletewebsite(id)
      .subscribe(() => this.removeWebsite(id));
  }

  checkWebsites() {
    this.configService
      .checkWebsites()
      .subscribe();
  }

  controls() {
    return this.addWebsiteForm.controls;
  }

  removeWebsite(id: number) {
    const removeIndex = this.websites.findIndex(item => item.id === id);
    this.websites.splice(removeIndex, 1);
  }
}
