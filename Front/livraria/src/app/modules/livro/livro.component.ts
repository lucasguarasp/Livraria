import { LivroService } from './../../shared/providers/livro.service';
import { Livro } from './../../shared/models/livro';
import { Autor } from './../../shared/models/autor';
import { AutorService } from './../../shared/providers/autor.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-livro',
  templateUrl: './livro.component.html',
  styleUrls: ['./livro.component.css']
})
export class LivroComponent implements OnInit {

  public form: FormGroup;
  public livro: Livro = new Livro;
  public autores: Array<Autor>;
  public livros: Array<Livro> = new Array<Livro>();

  constructor(private formBuilder: FormBuilder,
    private autorService: AutorService,
    private livroService: LivroService
  ) { }

  ngOnInit(): void {
    this.criarFormulario();
    this.preencherFormulario();
    this.listarAutores();
    this.obterLivros();
  }

  private criarFormulario() {
    this.form = this.formBuilder.group({
      livroId: [0],
      // autor:[new Autor],
      idAutor: [0, Validators.required],
      quantidade: [0],
      estoque: [0]
    });

  }

  private obterLivros() {
    this.livroService.listar().subscribe(
      response => {
        if (response) {
          this.livros = response;
        } else {
          this.livros = new Array<Livro>();
        }
      },
      error => {
      }
    );
  }

  private preencherFormulario() {
    this.form.patchValue(this.livro);
  }

  private async listarAutores() {
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

  public enviarLivro() {
    const livro = this.form.value;

    if (livro.livroId) {
      this.alterar();
    } else {
      this.inserir(livro);
    }
  }

  private alterar() {
    const livro = this.form.value;
    this.livro = livro;
    let autor = new Autor();
    autor.autorId = Number(livro.idAutor);
    livro.autor = autor;
    livro.autor.autorId = Number(livro.idAutor);

    this.livroService.alterar(this.livro, this.livro.livroId).subscribe(
      response => {
        if (response) {
          this.livro = response;
          this.obterLivros();
        } else {
          this.livro = new Livro;
        }
      },
      error => {
      });
  }

  private inserir(livro: Livro) {
    let autor = new Autor();
    autor.autorId = Number(livro.idAutor);
    livro.autor = autor;
    livro.autor.autorId = Number(livro.idAutor);
    this.livroService.inserir(livro).subscribe(
      response => {
        if (response) {
          this.livro = response;
          this.livros.push(this.livro);
          this.preencherFormulario();
        } else {
          this.livro = new Livro;
        }
      },
      error => {
      }
    );
  }

  public deletarLivro(livroId: number) {
    this.livroService.excluir(livroId).subscribe(
      response => {
        if (response) {
          const index = this.livros.findIndex(p => p.livroId === livroId);
          this.livros.splice(index, 1);
        } else {
          this.livro = new Livro;
        }
      },
      error => {
      }
    );

  }

  public editarLivro(livroId: number) {
    const livro = this.livros.find(p => p.livroId === livroId);
    this.livro = livro;
    this.preencherFormulario();
  }

  public comprarLivro(livroId: number) {
    //this.editarLivro(livroId);
    this.livroService.comprar(livroId).subscribe(
      response => {
        if (response) {
          // this.livro = response;
          this.livros.find(p => p.livroId == livroId).quantidade -= 1;
        } else {
          this.livro = new Livro;
        }
      },
      error => {
      });
  }

  public limparFormulario() {
    this.livro = new Livro;
    this.criarFormulario();
  }
}
