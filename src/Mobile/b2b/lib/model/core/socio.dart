import 'socio_empresa.dart';

class Socio {
  final List<SocioEmpresa> socioEmpresas;
  final String nome;
  final String cnpjCpf;
  final int codTipo;

  Socio(this.socioEmpresas, this.nome, this.cnpjCpf, this.codTipo);

  Socio.fromJson(Map<String, dynamic> json)
      : nome = json['nome'],
        cnpjCpf = json['cnpjCpf'],
        codTipo = json['codTipo'],
        socioEmpresas = json['socioEmpresas'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() => <String, dynamic>{
        'nome': nome,
        'cnpjCpf': cnpjCpf,
        'codTipo': codTipo,
      };
}
