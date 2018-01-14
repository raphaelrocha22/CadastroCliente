$(document).ready(function () {

    $('#txtDataInicio').datetimepicker({
        locale: 'pt-br',
        format: 'DD/MM/YYYY'
    });

    $('#txtDataFim').datetimepicker({
        locale: 'pt-br',
        format: 'DD/MM/YYYY'
    });

    $('#btnConsultar').click(function () {

        var model = {
            codCliente: $('#txtCodCliente').val(),
            codun: $('#txtCodun').val(),
            razaoSocial: $('#txtRazaoSocial').val(),
            nomeFantasia: $('#txtNomeFantasia').val(),
            cnpj: $('#txtCnpj').val().replace(/\.|-|[/]/g, ''),
            dataInicio: $('#txtDataInicio').val(),
            dataFim: $('#txtDataFim').val() + " 23:59:59",
            representante: {
                idRepresentante: $('#optRepresentante').val()
            }
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

                    conteudo += "<td class='hidden'>" + m.idCliente + "</td>";
                    conteudo += "<td>" + m.codCliente + "</td>";
                    conteudo += "<td>" + m.codun + "</td>";
                    conteudo += "<td>" + m.razaoSocial + "</td>";
                    conteudo += "<td>" + m.nomeFantasia + "</td>";
                    conteudo += "<td>" + m.classe + "</td>";
                    conteudo += "<td>" + m.representante.nome + "</td>";
                    conteudo += "<td>";
                    conteudo += "<button data-target='#janelaDetalhes' data-toggle='modal' onclick='exibirDetalhes(" + m.idCliente + ")' class='btn btn-success btn-sm'>Detalhes</button>";
                    conteudo += "&nbsp;";
                    conteudo += "<a href='/AreaRestrita/Cliente/Edicao/" + m.idCliente + "' class='btn btn-primary btn-sm'>Editar</a>";

                    conteudo += "</tr>";
                });
                $("#tabela tbody").html(conteudo);
                $("#quantidade").html(lista.length);
            },
            error: function (e) {
                console.log(e.status);
            },
            complete: function () {
                $('#btnConsultar').prop('value', 'Consultar').prop('disabled', false);
            }
        });
    });
});

function exibirDetalhes(id) {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/Cliente/Consulta',
        data: model = {
            idCliente: id,
            //abrir espaco de memoria para representante
            representante: {
                idRepresentante: 0
            }
        },
        success: function (model) {
            $.each(model, function (i, m) {

                $("#detalhes_cliente_dataCadastro").html(m.dataCadastro);
                $("#detalhes_codCliente").html(m.codCliente);
                $("#detalhes_codun").html(m.codun);
                $("#detalhes_razaoSocial").html(m.razaoSocial);
                $("#detalhes_nomeFantasia").html(m.nomeFantasia);
                $("#detalhes_CNPJ").html(m.cnpj);
                $("#detalhes_estadual").html(m.inscricaoEstadual);
                $("#detalhes_municipal").html(m.inscricaoMunicipal);
                $("#detalhes_classe").html(m.classe);
                $("#detalhes_representante").html(m.representante.nome);

                $("#detalhes_cadastro_dataCadastro").html(m.enderecoCadastro.dataCadastro);
                $("#detalhes_cadastro_logradouro").html(m.enderecoCadastro.logradouro);
                $("#detalhes_cadastro_numero").html(m.enderecoCadastro.numero);
                $("#detalhes_cadastro_complemento").html(m.enderecoCadastro.complemento);
                $("#detalhes_cadastro_bairro").html(m.enderecoCadastro.bairro);
                $("#detalhes_cadastro_municipio").html(m.enderecoCadastro.municipio);
                $("#detalhes_cadastro_uf").html(m.enderecoCadastro.UF);
                $("#detalhes_cadastro_cep").html(m.enderecoCadastro.cep);
                $("#detalhes_cadastro_telefone1").html(m.enderecoCadastro.telefone1);
                $("#detalhes_cadastro_telefone2").html(m.enderecoCadastro.telefone2);
                $("#detalhes_cadastro_email").html(m.enderecoCadastro.email);

                $("#detalhes_cobranca_dataCadastro").html(m.enderecoCobranca.dataCadastro);
                $("#detalhes_cobranca_logradouro").html(m.enderecoCobranca.logradouro);
                $("#detalhes_cobranca_numero").html(m.enderecoCobranca.numero);
                $("#detalhes_cobranca_complemento").html(m.enderecoCobranca.complemento);
                $("#detalhes_cobranca_bairro").html(m.enderecoCobranca.bairro);
                $("#detalhes_cobranca_municipio").html(m.enderecoCobranca.municipio);
                $("#detalhes_cobranca_uf").html(m.enderecoCobranca.UF);
                $("#detalhes_cobranca_cep").html(m.enderecoCobranca.cep);
                $("#detalhes_cobranca_telefone1").html(m.enderecoCobranca.telefone1);
                $("#detalhes_cobranca_telefone2").html(m.enderecoCobranca.telefone2);
                $("#detalhes_cobranca_email").html(m.enderecoCobranca.email);

                $("#detalhes_entrega_dataCadastro").html(m.enderecoEntrega.dataCadastro);
                $("#detalhes_entrega_logradouro").html(m.enderecoEntrega.logradouro);
                $("#detalhes_entrega_numero").html(m.enderecoEntrega.numero);
                $("#detalhes_entrega_complemento").html(m.enderecoEntrega.complemento);
                $("#detalhes_entrega_bairro").html(m.enderecoEntrega.bairro);
                $("#detalhes_entrega_municipio").html(m.enderecoEntrega.municipio);
                $("#detalhes_entrega_uf").html(m.enderecoEntrega.UF);
                $("#detalhes_entrega_cep").html(m.enderecoEntrega.cep);
                $("#detalhes_entrega_telefone1").html(m.enderecoEntrega.telefone1);
                $("#detalhes_entrega_telefone2").html(m.enderecoEntrega.telefone2);
                $("#detalhes_entrega_email").html(m.enderecoEntrega.email);
            });
        },
        error: function (e) {
            alert(e.status);
        }
    });
}