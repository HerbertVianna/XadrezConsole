using XadrezConsole.tabuleiro;
namespace XadrezConsole.xadrez {
    class Rei :Peca {
        public Rei(Tabuleiro tabuleiro, Cor cor):base(tabuleiro, cor) {

        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tabuleiro.linhas, tabuleiro.colunas];

            Posicao posicao = new Posicao(0, 0);

            //norte
            posicao.definirValores(posicao.linha - 1, posicao.coluna);
            if (tabuleiro.posicaoValida(posicao)&&podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //nordeste
            posicao.definirValores(posicao.linha - 1, posicao.coluna+1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //leste
            posicao.definirValores(posicao.linha, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //sudeste
            posicao.definirValores(posicao.linha+1, posicao.coluna + 1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //sul
            posicao.definirValores(posicao.linha+1, posicao.coluna);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //so
            posicao.definirValores(posicao.linha + 1, posicao.coluna-1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //oeste
            posicao.definirValores(posicao.linha, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            //no
            posicao.definirValores(posicao.linha-1, posicao.coluna - 1);
            if (tabuleiro.posicaoValida(posicao) && podeMover(posicao)) {
                mat[posicao.linha, posicao.coluna] = true;
            }
            return mat;
        }

        private bool podeMover(Posicao posicao) {
            Peca p = tabuleiro.obterPeca(posicao);
            return p == null || p.cor != this.cor;
        }

        public override string ToString() {
            return "R";
        }
    }
}
