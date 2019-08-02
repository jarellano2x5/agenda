import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'usuario-comp',
  templateUrl: './usuario.component.html'
})
export class UsuarioComponent {
  show: boolean = true;
  lusu: usuario[] = [];
  usu: usuario;

  constructor(@Inject('BASE_URL') private burl: string, private http: HttpClient, private router: Router) {
    this.lista();
  }

  lista() {
    this.http.get<usuario[]>(this.burl + 'api/Usuario').subscribe(response => {
      this.lusu = response;
    }, error => console.error(error));
  }

  nuevo() {
    this.usu = { usuarioId: 0, nombre: '', apellido: '', telefono: '' };
    this.show = false;
  }

  elige(usu: usuario) {
    this.usu = usu;
    this.show = false;
  }

  ir(id: number, nm: string) {
    this.router.navigate(['/contacto', id, nm]);
  }

  guarda() {
    if (this.usu.usuarioId == 0) {
      this.http.post<usuario>(this.burl + 'api/Usuario', this.usu).subscribe(response => {
        this.usu = response;
      }, error => console.error(error));
    } else {
      this.http.put<usuario>(this.burl + 'api/Usuario', this.usu).subscribe(response => {
        this.usu = response;
      }, error => console.error(error));
    }
  }

  cancela() {
    this.lista();
    this.show = true;
  }
}

interface usuario {
  usuarioId: number;
  nombre: string;
  apellido: string;
  telefono: string;
}
