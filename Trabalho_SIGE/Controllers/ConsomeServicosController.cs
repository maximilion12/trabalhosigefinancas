using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_SIGE.Models;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using Trabalho_SIGE.Models.Entradas;

namespace Trabalho_SIGE.Controllers
{
    public class ConsomeServicosController : Controller
    {
        HttpClient client = new HttpClient();
        Retorno ret = new Retorno();

        private Banco_Conexao_Entities db = new Banco_Conexao_Entities();

        #region stringsConexao

        string pathProdutosRH = @"https://sigerh.azurewebsites.net/api/Produtos";
        string pathProdutosProd = @"";
        string pathProdutosVendas = @"https://sigemv.azurewebsites.net/api/Produtos";

        string pathEventosRH = @"https://sigerh.azurewebsites.net/api/EventoOrcamento";
        string pathEventosProd = @"";
        string pathEventosVendas = @"https://sigemv.azurewebsites.net/api/EventoOrcamento";

        string pathqtdProdProduzir = @"qtdProdProduzir";

        string pathVendasVendas = @"https://sigerh.azurewebsites.net/api/ProduEventoOrcamentotos";

        #endregion

        public bool ObtemProdutoSetor(string path, int setor)
        {

            try
            {
                List<ProdutoEntrada> listaCont = new List<ProdutoEntrada>();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(path);
                request.Method = "GET";
                WebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                using (Stream webStream = httpResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            //response = new JsonArray.parse(responseReader.ReadToEnd());

                            listaCont = JsonConvert.DeserializeObject<List<ProdutoEntrada>>(responseReader.ReadToEnd());

                            foreach (var item in listaCont)
                            {
                                Produto prod = db.Produto.SingleOrDefault(p => p.nome == item.nome);
                                if (prod == null)
                                {
                                    db.Produto.Add(new Produto() { nome = item.nome, dataCotacao = System.DateTime.Now, preco = item.preco });
                                    db.SaveChanges();
                                }
                                else
                                {
                                    prod.preco = item.preco;
                                    prod.dataCotacao = System.DateTime.Now;
                                    db.SaveChanges();
                                }

                            }

                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region Produção
        public bool ObtemPedidosProducao(string path = @"http://sigepm.azurewebsites.net/getAllProducaoPorMesTurno")
        {
            try
            {
                List<ProducaoEntrada> listaCont = new List<ProducaoEntrada>();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(path);
                request.Method = "GET";
                WebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                using (Stream webStream = httpResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            //response = new JsonArray.parse(responseReader.ReadToEnd());

                            listaCont = JsonConvert.DeserializeObject<List<ProducaoEntrada>>(responseReader.ReadToEnd());

                            foreach (var item in listaCont)
                            {
                                Produto produto = db.Produto.SingleOrDefault(p => p.nome == item.nomeProduto);

                                Producao producao = db.Producao.SingleOrDefault(p => p.mes == item.mes && p.idProduto == produto.id && p.turno == item.turno);

                                if (producao == null)
                                {
                                    if (produto != null)
                                    {
                                        db.Producao.Add(new Producao()
                                        {
                                            idProduto = produto.id,
                                            quantidade = item.quantidade,
                                            mes = item.mes,
                                            turno = item.turno
                                        });
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        public bool ObtemEventoSetor(string path, int setor)
        {
            try
            {
                List<EventoEntrada> listaCont = new List<EventoEntrada>();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(path);
                request.Method = "GET";
                WebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                using (Stream webStream = httpResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            //response = new JsonArray.parse(responseReader.ReadToEnd());

                            listaCont = JsonConvert.DeserializeObject<List<EventoEntrada>>(responseReader.ReadToEnd());

                            foreach (var item in listaCont)
                            {
                                Evento solicitacao = db.Evento.SingleOrDefault(p => p.idEvento == item.idEvento);
                                if (solicitacao == null)
                                {
                                    db.Evento.Add(new Evento()
                                    {
                                        idEvento = item.idEvento,
                                        orcamento = item.orcamento,
                                        gasto = item.gasto,
                                        fornecedor = item.fornecedor,
                                        idSetor = item.idSetor,
                                        data = item.data
                                    });
                                    db.SaveChanges();
                                }
                            }
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        DateTime ConvertToLastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public bool ObtemVendas(string path)
        {
            try
            {
                List<VendaEntrada> listaCont = new List<VendaEntrada>();

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(path);
                request.Method = "GET";
                WebResponse httpResponse = (HttpWebResponse)request.GetResponse();

                using (Stream webStream = httpResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            //response = new JsonArray.parse(responseReader.ReadToEnd());

                            listaCont = JsonConvert.DeserializeObject<List<VendaEntrada>>(responseReader.ReadToEnd());

                            foreach (var item in listaCont)
                            {
                                if (item == null)
                                {
                                    Conta_Receber novaconta = new Conta_Receber()
                                    {
                                        idTipo = 4,
                                        valor = (decimal)item.valor,
                                        vencimento = ConvertToLastDayOfMonth((System.DateTime)item.data),
                                    };
                                    db.Conta_Receber.Add(novaconta);
                                    db.SaveChanges();

                                    db.Registro_De_Venda.Add(new Registro_De_Venda()
                                    {
                                        codRegistro = item.idVenda.ToString(),
                                        data = (System.DateTime)item.data,
                                        idContaReceber = novaconta.id,
                                        valor = (decimal)item.valor
                                    });
                                    db.SaveChanges();

                                    db.Vendas.Add(new Vendas()
                                    {
                                        idVenda = item.idVenda,
                                        data = item.data == null ? System.DateTime.Now : item.data,
                                        valor = (decimal)item.valor
                                    });
                                    db.SaveChanges();
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [System.Web.Mvc.HttpGet]
        [ResponseType(typeof(Conta))]
        public async Task<JsonResult> GeraRotina()
        {
            string saida = "";
            try
            {
                #region ObtemProduto
                if (ObtemProdutoSetor(pathProdutosRH, 1))
                {
                    saida += "RHProdOK; ";
                }
                if (ObtemProdutoSetor(pathProdutosProd, 4))
                {
                    saida += "ProducaoProdOK; ";
                }
                if (ObtemProdutoSetor(pathProdutosVendas, 3))
                {
                    saida += "VendasProdOK; ";
                }
                #endregion


                #region ObtemEvento
                if (ObtemEventoSetor(pathEventosRH, 1))
                {
                    saida += "RHEventOK; ";
                }
                if (ObtemEventoSetor(pathEventosProd, 4))
                {
                    saida += "ProducaoEventOK; ";
                }
                if (ObtemEventoSetor(pathEventosVendas, 3))
                {
                    saida += "VendasEventOK; ";
                }
                #endregion


                #region ObtemVendas
                if (ObtemEventoSetor(pathVendasVendas, 3))
                {
                    saida += "VendasOK; ";
                }
                #endregion

                ret.retorno = true;
                ret.msg = saida;
                return Json(ret, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ret.retorno = false;
                ret.msg = ex.Message;
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
