$(document).ready(function () {

    $('.date').datepicker({
    });

    $('#btnConsultar').click(function () {
        Consultar();
    });
});

function Consultar() {

    var model = {
        CodCliente: $('#txtCodCliente').val(),
        Codun: $('#txtCodun').val(),
        RazaoSocial: $('#txtRazaoSocial').val(),
        NomeFantasia: $('#txtNomeFantasia').val(),
        Cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
        DataInicio: $('#txtDataInicio').val(),
        DataFim: $('#txtDataFim').val() + " 23:59:59",
        IdRepresentante: $('#optRepresentante').val()
    };
    $.ajax({
        type: "POST",
        url: "/AreaRestrita/Cliente/Consulta",
        data: model,
        beforeSend: function () {
            $('#btnConsultar').prop('value', 'Aguarde...').prop('disabled', true);
        },
        success: function (lista) {
            var conteudo = "";

            $.each(lista, function (i, m) {

                conteudo += "<tr>";
                
                conteudo += "<td>" + m.IdCliente + "</td>";
                conteudo += "<td style='max-width: 7ch' class='limiteColuna'>" + m.CodCliente + "</td>";
                conteudo += "<td style='max-width: 7ch' class='limiteColuna'>" + m.Codun + "</td>";
                conteudo += "<td style='max-width: 19ch' class='limiteColuna'>" + m.RazaoSocial + "</td>";
                conteudo += "<td style='max-width: 19ch' class='class='limiteColuna'>" + m.NomeFantasia + "</td>";
                conteudo += "<td style='max-width: 10ch' class='limiteColuna'>" + m.Classe + "</td>";
                conteudo += "<td style='max-width: 15ch' class='limiteColuna'>" + m.NomeRepresentante + "</td>";
                conteudo += "<td>";
                conteudo += "<button data-target='#janelaDetalhes' data-toggle='modal' onclick='exibirDetalhes(" + m.IdCliente + ")' class='btn btn-success btn-xs'>Detalhes</button>";
                conteudo += "&nbsp;";
                conteudo += "<a href='/AreaRestrita/Cliente/Edicao/" + m.IdCliente + "' class='btn btn-primary btn-xs'>Editar</a>";
                conteudo += "</td>";

                conteudo += "</tr>";
            });
            $("#tabela tbody").html(conteudo);
            $('#tabela').DataTable();
        },
        error: function (e) {
            alert(e.status);
        },
        complete: function () {
            $('#btnConsultar').prop('value', 'Consultar').prop('disabled', false);
        }
    });
};

function exibirDetalhes(id) {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/Cliente/Consulta',
        data: model = {
            IdCliente: id
        },
        success: function (model) {
            $.each(model, function (i, m) {

                $("#detalhes_cliente_dataCadastro").html(m.DataCadastro);
                $("#detalhes_codCliente").html(m.CodCliente);
                $("#detalhes_codun").html(m.Codun);
                $("#detalhes_razaoSocial").html(m.RazaoSocial);
                $("#detalhes_nomeFantasia").html(m.NomeFantasia);
                $("#detalhes_CNPJ").html(m.Cnpj);
                $("#detalhes_estadual").html(m.InscricaoEstadual);
                $("#detalhes_municipal").html(m.InscricaoMunicipal);
                $("#detalhes_classe").html(m.Classe);
                $("#detalhes_representante").html(m.NomeRepresentante);

                $("#detalhes_cadastro_dataCadastro").html(m.EnderecoCadastro.DataCadastro);
                $("#detalhes_cadastro_logradouro").html(m.EnderecoCadastro.Logradouro);
                $("#detalhes_cadastro_numero").html(m.EnderecoCadastro.Numero);
                $("#detalhes_cadastro_complemento").html(m.EnderecoCadastro.Complemento);
                $("#detalhes_cadastro_bairro").html(m.EnderecoCadastro.Bairro);
                $("#detalhes_cadastro_municipio").html(m.EnderecoCadastro.Municipio);
                $("#detalhes_cadastro_uf").html(m.EnderecoCadastro.UF);
                $("#detalhes_cadastro_cep").html(m.EnderecoCadastro.Cep);
                $("#detalhes_cadastro_telefone1").html(m.EnderecoCadastro.Telefone1);
                $("#detalhes_cadastro_telefone2").html(m.EnderecoCadastro.Telefone2);
                $("#detalhes_cadastro_email").html(m.EnderecoCadastro.Email);

                $("#detalhes_cobranca_dataCadastro").html(m.EnderecoCobranca.DataCadastro);
                $("#detalhes_cobranca_logradouro").html(m.EnderecoCobranca.Logradouro);
                $("#detalhes_cobranca_numero").html(m.EnderecoCobranca.Numero);
                $("#detalhes_cobranca_complemento").html(m.EnderecoCobranca.Complemento);
                $("#detalhes_cobranca_bairro").html(m.EnderecoCobranca.Bairro);
                $("#detalhes_cobranca_municipio").html(m.EnderecoCobranca.Municipio);
                $("#detalhes_cobranca_uf").html(m.EnderecoCobranca.UF);
                $("#detalhes_cobranca_cep").html(m.EnderecoCobranca.Cep);
                $("#detalhes_cobranca_telefone1").html(m.EnderecoCobranca.Telefone1);
                $("#detalhes_cobranca_telefone2").html(m.EnderecoCobranca.Telefone2);
                $("#detalhes_cobranca_email").html(m.EnderecoCobranca.Email);

                $("#detalhes_entrega_dataCadastro").html(m.EnderecoEntrega.DataCadastro);
                $("#detalhes_entrega_logradouro").html(m.EnderecoEntrega.Logradouro);
                $("#detalhes_entrega_numero").html(m.EnderecoEntrega.Numero);
                $("#detalhes_entrega_complemento").html(m.EnderecoEntrega.Complemento);
                $("#detalhes_entrega_bairro").html(m.EnderecoEntrega.Bairro);
                $("#detalhes_entrega_municipio").html(m.EnderecoEntrega.Municipio);
                $("#detalhes_entrega_uf").html(m.EnderecoEntrega.UF);
                $("#detalhes_entrega_cep").html(m.EnderecoEntrega.Cep);
                $("#detalhes_entrega_telefone1").html(m.EnderecoEntrega.Telefone1);
                $("#detalhes_entrega_telefone2").html(m.EnderecoEntrega.Telefone2);
                $("#detalhes_entrega_email").html(m.EnderecoEntrega.Email);
            });
        },
        error: function (e) {
            alert(e.status);
        }
    });
};