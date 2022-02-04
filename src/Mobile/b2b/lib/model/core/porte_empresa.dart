class PorteEmpresa {
  final int cod;
  final String descricao;

  PorteEmpresa(this.cod, this.descricao);

  PorteEmpresa.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'cod': cod,
        'descricao': descricao,
      };
}
