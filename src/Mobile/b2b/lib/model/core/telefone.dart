class Telefone {
  final String numero;
  final bool isFax;

  Telefone(this.numero, this.isFax);

  Telefone.fromJson(Map<String, dynamic> json)
      : numero = json['numero'],
        isFax = json['isFax'] ?? false;
}
