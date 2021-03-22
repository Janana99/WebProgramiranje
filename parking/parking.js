import {Polje} from "./polje.js"
export class Parking{
    constructor(idpark, n, m, ime, kapacitet){
        this.id=idpark;
        this.n=n;
        this.m=m;
        this.ime=ime;
        this.kapacitet=kapacitet;
        this.kontNaslov=null;
  //      this.kontZaFormu=null;
        this.kontejner=null;
        this.parkingPolja=[];
    }

    dodajPolje(polje){
        this.parkingPolja.push(polje);
    }

    crtajPolja(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");
            
        const kontPolja=document.createElement("div");
        kontPolja.className="kontPolja";
        host.appendChild(kontPolja);

        let red;
        let polje;
        for(let i=0; i<this.n; i++){
            red=document.createElement("div");
            red.className="red";
            kontPolja.appendChild(red);

            for(let j=0; j<this.m; j++){
                polje=new Polje(i, j, "", this.kapacitet);
                this.dodajPolje(polje);
                polje.crtajPolje(red);
                
            }
        }
    }

    crtajParking(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");

        this.kontNaslov=document.createElement("div");
        this.kontNaslov.className="kontNaslov";
        host.appendChild(this.kontNaslov);

  //      this.kontZaFormu=document.createElement("div");
  //      this.kontZaFormu.className="kontZaFormu";
  //      host.appendChild(this.kontZaFormu);
      
        this.kontejner=document.createElement("div");
        this.kontejner.className="kontejner";
        host.appendChild(this.kontejner);

        this.crtajNaslov(this.kontNaslov);
        this.crtajFormu(this.kontejner);
        this.crtajPolja(this.kontejner);

        fetch("https://localhost:5001/Parking/PreuzmiPolje").then(p => {
            p.json().then(data => {
                data.forEach(polje => {
                    if(polje.idparkinga==this.id)
                        this.parkingPolja[polje.x * this.m + polje.y].azurirajPolje(polje.id, polje.x, polje.y, polje.nazivpolja, polje.brojautomobila, polje.maxkapacitet, this.id);
                });
            });
        });
    }

    crtajNaslov(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");

        var labela1=document.createElement("label");
        labela1.className="labela1";
        labela1.innerHTML="PARKING";
        host.appendChild(labela1);
    }

    crtajFormu(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");

        const kontForma=document.createElement("div");
        kontForma.className="kontForma";
        host.appendChild(kontForma);

        var labela2=document.createElement("label");
        labela2.className="labela2";
        labela2.innerHTML="Unesite naziv polja:";
        kontForma.appendChild(labela2);
        let tb=document.createElement("input");
        tb.className="nazivPolja";
        kontForma.appendChild(tb);

        let novDiv=document.createElement("div");
        novDiv.className="novDiv";
        let selX=document.createElement("select");
        labela2=document.createElement("label");
        labela2.innerHTML="X:";
        novDiv.appendChild(labela2);
        novDiv.appendChild(selX);
        let opcija=null;
        for(let i=0; i< this.n; i++)
        {
            opcija=document.createElement("option");
            opcija.innerHTML=i;
            opcija.value=i;
            selX.appendChild(opcija);
        }
        kontForma.appendChild(novDiv);

        let selY=document.createElement("select");
        labela2=document.createElement("label");
        labela2.innerHTML="Y:";
		novDiv.appendChild(labela2);
        novDiv.appendChild(selY);
        let opc=null;
        for(let j=0; j< this.m; j++)
        {
            opc=document.createElement("option");
            opc.innerHTML=j;
            opc.value=j;
            selY.appendChild(opc);
        }
        kontForma.appendChild(novDiv);

        let dugmeDodaj=document.createElement("button");
        dugmeDodaj.className="dugmeDodaj";
        dugmeDodaj.innerHTML="Dodaj polje.";
        kontForma.appendChild(dugmeDodaj);

        let dugmeAzuriraj=document.createElement("button");
        dugmeAzuriraj.className="dugmeAzuriraj";
        dugmeAzuriraj.innerHTML="Ažuriraj polje."
        kontForma.appendChild(dugmeAzuriraj);

        let dugmeIzbrisi=document.createElement("button");
        dugmeIzbrisi.className="dugmeIzbrisi";
        dugmeIzbrisi.innerHTML="Izbriši polje.";
        kontForma.appendChild(dugmeIzbrisi);
    
        dugmeDodaj.onclick =(ev) => {
            const imePolja=this.kontejner.querySelector(".nazivPolja").value;
            let x=parseInt(selX.value);
            let y=parseInt(selY.value);

            fetch("https://localhost:5001/Parking/UpisiPolje/" +this.id, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    idpolja: this.id,
                    x: x,
                    y: y,
                    nazivpolja: imePolja
                })
            }).then(p => {
                if(p.ok){
                    fetch("https://localhost:5001/Parking/PreuzmiPolje").then(p => {
                        p.json().then(data => {
                            data.forEach(polje => {
                                if(polje.idparkinga===this.id)
                                    this.parkingPolja[polje.x * this.m +polje.y].azurirajPolje(polje.idpolja, polje.x, polje.y, polje.nazivpolja, polje.brojautomobila, polje.maxkapacitet, this.id);
                            });
                        });
                    });
                } else if(p.status==400) {
                    const greskap= {x:0, y:0};
                    p.json().then(r => {
                        greskap.x=r.x;
                        greskap.y=r.y;
                        alert("Postoji nepopunjena lokacija sa navedenim poljem! Lokacija je (" +greskap.x+ ", " +greskap.y+ ")");
                    });
                } else {
                    alert("Greška prilikom upisa.");
                }
            }).catch(p => {
                alert("Greška prilikom upisa.");
            });
        }

        dugmeAzuriraj.onclick = (ev) =>{
            const ip=this.kontejner.querySelector(".nazivPolja").value;
            let x1=parseInt(selX.value);
            let y1=parseInt(selY.value);
            const idpp=this.idpolja;

            fetch("https://localhost:5001/Parking/IzmeniPolje" , {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    idpolja: this.id,
                    x: x1,
                    y: y1,
                    nazivpolja: ip
                })
            }).then(p => {
                if(p.ok){
                    this.parkingPolja[x1 * this.m + y1].azurirajIme(ip);
                } else 
                 {
                    alert("Greška prilikom upisa.");
                }
            }).catch(p => {
                console.log(p);
                alert("Greška prilikom upisa.");
            });
        }

        dugmeIzbrisi.onclick = (ev) =>{
            const ip=this.kontejner.querySelector(".nazivPolja").value;
            let x2=parseInt(selX.value);
            let y2=parseInt(selY.value);
            let potencijalnoPolje=this.parkingPolja.find(el=>el.nazivpolja==ip);
            console.log(potencijalnoPolje);

            fetch("https://localhost:5001/Parking/IzbrisiPolje/" + potencijalnoPolje.id, {
                headers: {
                    'Access-Token': 'token'
                },
                method: 'delete'
            }).then((data) => {
                data.text().then(text => console.log(text));
            }).catch(e => {
                console.log(e);
                return e;
            });
        }

    }
}