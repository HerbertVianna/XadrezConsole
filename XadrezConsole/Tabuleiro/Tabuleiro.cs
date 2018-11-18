
namespace XadrezConsole.tabuleiro {
    class Tabuleiro {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            this.pecas = new Peca[linhas, colunas];
        }

        public Peca obterPeca(int linha, int coluna) {
            return pecas[linha, coluna];
        }

        public void adicionarPeca (Peca p, Posicao posicao) {
            pecas[posicao.linha, posicao.coluna] = p;
        }
    }
}
