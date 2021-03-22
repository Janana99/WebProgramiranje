import {Automobil} from "./automobil.js"

export class Polje{
    constructor(a, b, naziv, maxKap, _idparkinga, idp){
        this.id=idp;
        this.x=a;
        this.y=b;
        this.boja="#d8e2dc";
        this.nazivpolja=naziv;
        this.brojautomobila=0;
        this.maxkapacitet=maxKap;
        this.idparkinga=_idparkinga;
        this.miniKontejner=null;
        this.automobili=[];
    }

    crtajPolje(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");

        this.miniKontejner=document.createElement("div");
        this.miniKontejner.className="miniKontejner";
        this.miniKontejner.innerHTML="Polje "+this.brojautomobila+ ", "+this.maxkapacitet;
        host.appendChild(this.miniKontejner);

        this.crtajFormuPolja(this.miniKontejner);
        fetch("https://localhost:5001/Parking/PreuzmiAutomobil").then(p => {
            p.json().then(data => {
                data.forEach(auto => {
                    if(auto.idpolja==this.id){
                        var au=new Automobil(auto.idpolja, auto.tip, auto.automobilibroj);
                        this.dodajAutomobil(au);
                    }
                });
            });
        });
        this.crtajAutomobile();
    }

    dodajAutomobil(auto){
        this.automobili.push(auto);
    }

    crtajAutomobile(){
        this.automobili.forEach(auto =>{
            auto.crtajAutomobil(this.miniKontejner);
        });
    }

    crtajFormuPolja(host)
    {
        if(!host)
            throw new Exception("Roditeljski element ne postoji");

        const formaPolja=document.createElement("div");
        formaPolja.className="formaPolja";
        host.appendChild(formaPolja);

        var lab1=document.createElement("label");
        lab1.innerHTML="Unesite tip automobila:";
        formaPolja.appendChild(lab1);
        let tb=document.createElement("input");
        tb.className="tipAutomobila";
        formaPolja.appendChild(tb);

        lab1=document.createElement("label");
        lab1.innerHTML="Unesite broj automobila";
        formaPolja.appendChild(lab1);
        tb=document.createElement("input");
        tb.className="brojAutomobila";
        tb.type="number";
        formaPolja.appendChild(tb);

        const buttonUpisi=document.createElement("button");
        buttonUpisi.className="buttonUpisi";
        buttonUpisi.innerHTML="Dodaj automobil";
        formaPolja.appendChild(buttonUpisi);

        // const buttonIzbrisi=document.createElement("button");
        // buttonIzbrisi.className="buttonIzbrisi";
        // buttonIzbrisi.innerHTML="Izbriši automobil";
        // formaPolja.appendChild(buttonIzbrisi);

        buttonUpisi.onclick = (ev) => {
            const idPo=this.id;
            console.log(this.id);
            const tippAuto=this.miniKontejner.querySelector(".tipAutomobila").value;
            const brojjAuto=parseInt(this.miniKontejner.querySelector(".brojAutomobila").value);
            fetch("https://localhost:5001/Parking/UpisiAutomobil/" + `${idPo}` , {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    tip: tippAuto,
                    automobilibroj: brojjAuto,
                    idpolja: this.id
                })
            }).then(s => { 
                if(s.ok){
                    var auto= new Automobil(idPo, tippAuto, brojjAuto);
                    this.dodajAutomobil(auto);
                    auto.crtajAutomobil(this.miniKontejner);
                    this.azurirajPolje(this.id, this.x, this.y, this.nazivpolja, this.brojautomobila + brojjAuto, this.maxkapacitet, this.idparkinga);
                }
                else if(s==400){
                    alert("Kapacitet popunjen!");
                }
                else {
                    console.log(s);
                    alert("Kapacitet je popunjen!");
                }
            });
        }      
    }

    azurirajPolje(id, x, y, ime, broj, kapacitet, idparking){
        console.log("Pozvano azuriranje");
            this.id=id;
            this.x=x;
            this.y=y;
            this.nazivpolja=ime;
            this.brojautomobila=broj;
            this.maxkapacitet=kapacitet;
            this.idparkinga=idparking;

            this.miniKontejner.innerHTML= this.nazivpolja + " " +this.brojautomobila + ", "+kapacitet;
            this.miniKontejner.style.backgroundColor="#e01e37";
            this.crtajFormuPolja(this.miniKontejner);

            fetch("https://localhost:5001/Parking/PreuzmiAutomobil").then(r => {
                r.json().then(data => {
                    data.forEach(auto => {
                        var a=new Automobil(auto.idpolja, auto.tip, auto.automobilibroj);
                        if(a.idpolja==this.id)
                            a.crtajAutomobil(this.miniKontejner);
                    });
                });
            });
    }

    azurirajIme(ime){
        this.nazivpolja=ime;
        this.azurirajPolje(this.id, this.x, this.y, this.nazivpolja, this.brojautomobila, this.maxkapacitet, this.idparkinga);
    }

    obrisiPolje(){
        this.miniKontejner.innerHTML="Polje "+this.brojautomobila+ ", "+this.maxkapacitet;
    }
}