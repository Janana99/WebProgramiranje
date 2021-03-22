export class Automobil{
    constructor( _idpolja, _tip, broj){
        this.tip=_tip;
        this.automobilibroj=broj;
        this.idpolja=_idpolja;
        this.miniKont=null;
    }

    crtajAutomobil(host){
        if(!host)
            throw new Exception("Roditeljski element ne postoji");
        
        this.miniKont=document.createElement("div");
        this.miniKont.className="miniKont";
        this.miniKont.innerHTML=this.tip + ", " +this.automobilibroj;
        host.appendChild(this.miniKont);
    }
  

    azurirajAutomobil(br) {
          this.automobilibroj+=br;
          this.miniKont.innerHTML=this.tip + ", " +this.automobilibroj;
    }
}