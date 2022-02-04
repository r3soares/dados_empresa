class Telefone {
  final String numero;
  final bool isFax;

  Telefone(this.numero, this.isFax);

  Telefone.fromJson(Map<String, dynamic> json)
      : numero = json['numero'],
        isFax = json['isFax'] ?? false;

  Map<String, dynamic> toMap() => <String, dynamic>{'numero': numero, 'isFax': isFax ? 1 : 0};
}
