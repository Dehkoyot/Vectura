import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../shared/user.service';
import { Visit } from '../shared/visit.model';

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit {

  lat: number = 49.851142;
  lng: number = 23.989991;
  locationChosen = false;



  constructor() { }

  ngOnInit() {

  }

  onChosenLocation(event){

    this.lat = event.coords.lat;
    this.lng = event.coords.lng; 
    this.locationChosen =true;
  }


}
