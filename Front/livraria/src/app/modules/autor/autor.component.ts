import { AutorService } from './../../shared/providers/autor.service';
import { Autor } from './../../shared/models/autor';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-autor',
  templateUrl: './autor.component.html',
  styleUrls: ['./autor.component.css']
})
export class AutorComponent implements OnInit {

  public form: FormGroup;
  public autor: Autor = new Autor;
  public autores: Array<Autor> = new Array<Autor>();

  constructor(private formBuilder: FormBuilder,
    private autorService: AutorService) { }

  ngOnInit(): void {
    this.criarFormulario();
    this.preencherFormulario();
    this.obterAutores();
  }

  private criarFormulario() {
    this.form = this.formBuilder.group({
      autorId: [0],
      nome: [null, Validators.required]
    });

  }

  private obterAutores() {
    this.autorService.listar().subscribe(
      response => {
        if (response) {
          this.autores = response;
        } else {
          this.autores = new Array<Autor>();
        }
      },
      error => {
      }
    );
  }

  private preencherFormulario() {
    this.form.patchValue(this.autor);
  }

  public enviarAutor() {
    const autor = this.form.value;

    if (autor.autorId) {
      this.alterar();
    } else {
      this.inserir(autor);
    }
  }

  private alterar() {
    const autor = this.form.value;
    this.autor = autor;
    this.autorService.alterar(this.autor, this.autor.autorId).subscribe(
      response => {
        if (response) {
          this.autor = response;
          this.preencherFormulario();
          this.obterAutores();
        } else {
          this.autor = new Autor;
        }
      },
      error => {
      }
    );
  }


  private inserir(autor: Autor) {
    if (autor.nome != null) {
      this.autorService.inserir(autor).subscribe(
        response => {
          if (response) {
            this.autor = response;
            this.preencherFormulario();
            this.obterAutores();
          } else {
            this.autor = new Autor;
          }
        },
        error => {
        }
      );
    }
  }

  public deletarAutor(autorId: number) {
    this.autorService.excluir(autorId).subscribe(
      response => {
        if (response) {
          const index = this.autores.findIndex(p => p.autorId === autorId);
          this.autores.splice(index, 1);
        } else {
          this.autor = new Autor;
        }
      },
      error => {
      }
    );

  }

  public editarAutor(autorId: number) {
    const autor = this.autores.find(p => p.autorId === autorId);
    this.autor = autor;
    this.preencherFormulario();
  }

  public limparFormulario() {
    this.autor = new Autor;
    this.criarFormulario();
  }


}
