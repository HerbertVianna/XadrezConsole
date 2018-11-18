
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

        public Peca obterPeca(Posicao posicao) {
            return pecas[posicao.linha, posicao.coluna];
        }

        public Peca obterPeca(int linha, int coluna) {
            return pecas[linha, coluna];
        }

        public void adicionarPeca (Peca p, Posicao posicao) {
            if (existePeca(posicao)) {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            pecas[posicao.linha, posicao.coluna] = p;
            p.posicao = posicao;

        }

        public Peca removerPeca (Posicao posicao) {
            if (obterPeca(posicao) == null) {
                return null;
            }
            Peca aux = obterPeca(posicao);
            aux.posicao = null;
            this.pecas[posicao.linha, posicao.coluna]=null;
            return aux;
        }

        public bool posicaoValida(Posicao posicao) {
            if (posicao.linha<0 || posicao.linha>= this.linhas || posicao.coluna <0 || posicao.coluna >= this.colunas) {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos) {
            if (!posicaoValida(pos)){
                throw new TabuleiroException("Posicao Inválida");
            }
        }

        public bool existePeca(Posicao pos) {
            validarPosicao(pos);
            return obterPeca(pos) != null;
        }

    }
}
