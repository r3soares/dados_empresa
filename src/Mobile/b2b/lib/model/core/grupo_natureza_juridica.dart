import 'natureza_juridica.dart';

class GrupoNaturezaJuridica {
  /// <summary>
  /// 1. Administração Pública
  /// 2. Entidades Empresariais
  /// 3. Entidades sem Fins Lucrativos
  /// 4. Pessoas Físicas
  /// 5. Organizações Internacionais e Outras Instituições Extraterritoriais
  /// </summary>
  final int cod;
  final String descricao;
  final List<NaturezaJuridica> lista;

  GrupoNaturezaJuridica(this.cod, this.descricao, this.lista);
}
