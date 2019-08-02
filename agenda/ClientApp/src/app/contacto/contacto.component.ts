import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'contacto-comp',
  templateUrl: './contacto.component.html'
})
export class ContactoComponent implements OnInit, OnDestroy {
  show: boolean = true;
  nav: any;
  usuid: number = 0;
  usunm: string = '';
  lcon: contacto[] = [];
  con: contacto;
  ltel: telefono[] = [];
  tel: telefono;

  constructor(@Inject('BASE_URL') private burl: string, private http: HttpClient, private route: ActivatedRoute) {

  }

  lista() {
    this.http.get<contacto[]>(this.burl + 'api/Contacto/' + this.usuid).subscribe(response => {
      this.lcon = response;
    }, error => console.error(error));
  }

  listatel() {
    this.http.get<telefono[]>(this.burl + 'api/Telefono/' + this.con.contactoId).subscribe(response => {
      this.ltel = response;
    }, error => console.error(error));
  }

  nuevo() {
    this.con = { contactoId: 0, usuarioId: this.usuid, nombre: '', empresa: '' };
    this.ltel = [];
    this.show = false;
  }

  nuevotel() {
    this.tel = { contactoId: this.con.contactoId, telefonoId: 0, tipo: '', numero: '' };
  }

  elige(cc: contacto) {
    this.con = cc;
    this.listatel();
    this.nuevotel();
    this.show = false;
  }

  elitel(tt: telefono) {
    this.tel = tt;
  }

  guarda() {
    if (this.con.contactoId == 0) {
      this.http.post<contacto>(this.burl + 'api/Contacto', this.con).subscribe(response => {
        this.con = response;
        this.listatel();
        this.nuevotel();
      }, error => console.error(error));
    } else {
      this.http.put<contacto>(this.burl + 'api/Contacto', this.con).subscribe(response => {
        this.con = response;
      }, error => console.error(error));
    }
  }

  guardatel() {
    if (this.tel.telefonoId == 0) {
      this.http.post<telefono>(this.burl + 'api/Telefono', this.tel).subscribe(response => {
        this.nuevotel();
        this.listatel();
      }, error => console.error(error));
    } else {
      this.http.put<telefono>(this.burl + 'api/Telefono', this.tel).subscribe(response => {
        this.nuevotel();
        this.listatel();
      }, error => console.error(error));
    }
  }

  borra(id: number) {
    this.http.delete<Boolean>(this.burl + 'api/Contacto/' + id).subscribe(response => {
      console.log(response);
      this.lista();
    }, error => console.error(error));
  }

  del(id: number) {
    this.http.delete<Boolean>(this.burl + 'api/Telefono/' + id).subscribe(response => {
      console.log(response);
      this.listatel();
    }, error => console.error(error));
  }

  cancela() {
    this.show = true;
    this.lista();
  }

  ngOnInit() {
    this.nav = this.route.params.subscribe(parm => {
      this.usuid = +parm['idu'];
      this.usunm = parm['nmu'];
      this.lista();
    });
  }

  ngOnDestroy() {
    this.nav.unsubscribe();
  }

}

interface contacto {
  contactoId: number;
  nombre: string;
  empresa: string;
  usuarioId: number;
}

interface telefono {
  telefonoId: number;
  contactoId: number;
  tipo: string;
  numero: string;
}
