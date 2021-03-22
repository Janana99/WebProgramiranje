import {Parking} from "./parking.js";

fetch("https://localhost:5001/Parking/PreuzmiParking").then(p => {
    p.json().then(data => {
        data.forEach(parking => {
            const parking1= new Parking(parking.id, parking.n, parking.m, parking.ime, parking.kapacitet);
            parking1.crtajParking(document.body);
           
        });
    });
});