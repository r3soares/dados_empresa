//This is where all our application constants will be present and this is different for each application.
class Constants {
  static final String databaseName = 'base.db';
  static final List<String> createTables = [
    '''CREATE TABLE estado (uf TEXT PRIMARY KEY)''',
    '''CREATE TABLE municipio (id INTEGER PRIMARY KEY, nome TEXT, uf TEXT)''',
    '''CREATE TABLE empresa (cnpj TEXT PRIMARY KEY, 
          isMatriz INTEGER, 
          razaoSocial TEXT, 
          nomeFantasia TEXT, 
          codSituacao INTEGER,
          codMotivo INTEGER,
          dataSituacao INTEGER,
          codNaturezaJuridica INTEGER,
          dataInicioAtividade INTEGER,
          codCnaeFiscal INTEGER,
          codMunicipio INTEGER,
          codPorte INTEGER,
          capitalSocial REAL)''',
    '''CREATE TABLE endereco (id TEXT PRIMARY KEY, logradouro TEXT, numero TEXT, complemento TEXT, bairro TEXT, cep TEXT)''',
    '''CREATE TABLE contato (id TEXT PRIMARY KEY, telefones TEXT, email TEXT)'''
  ];
}
