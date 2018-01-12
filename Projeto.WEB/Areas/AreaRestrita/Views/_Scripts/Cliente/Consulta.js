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

                    conteudo += "<td><style hidden>" + m.idCliente + "</style></td>";
                    conteudo += "<td>" + m.codCliente + "</td>";
                    conteudo += "<td>" + m.codun + "</td>";
                    conteudo += "<td>" + m.razaoSocial + "</td>";
                    conteudo += "<td>" + m.nomeFantasia + "</td>";
                    conteudo += "<td>" + m.classe + "</td>";
                    conteudo += "<td>" + m.representante.nome + "</td>";
                    conteudo += "<td>";
                    conteudo += "<button data-target='#janelaDetalhes' data-toggle='modal' onclick='exibirDetalhes(" + m.idCliente + ")' class='btn btn-success btn-sm'>Detalhes</button>";
                    conteudo += "&nbsp;";
                    conteudo += "<a href='/AreaRestrita/Cliente/Index' class='btn btn-primary btn-sm'>Detalhes</a>"

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
            },
        });
    });
});

function exibirDetalhes(id) {
    $.ajax({
        type: "POST",
        url: '/AreaRestrita/Cliente/ObterCliente',
        data: "id=" + id,
        success: function (model) {
            $("#detalhes_codCliente").html(model.codCliente);
            $("#detalhes_codun").html(model.codun);
            $("#detalhes_razaoSocial").html(model.razaoSocial);
            $("#detalhes_nomeFantasia").html(model.nomeFantasia);
            $("#detalhes_CNPJ").html(model.cnpj);
            $("#detalhes_estadual").html(model.inscricaoEstadual);
            $("#detalhes_municipal").html(model.inscricaoMunicipal);
            $("#detalhes_classe").html(model.classe);
            $("#detalhes_representante").html(model.representante.nome);

            $("#detalhes_cadastro_logradouro").html(model.enderecoCadastro.logradouro);
            $("#detalhes_cadastro_numero").html(model.enderecoCadastro.numero);
            $("#detalhes_cadastro_complemento").html(model.enderecoCadastro.complemento);
            $("#detalhes_cadastro_bairro").html(model.enderecoCadastro.bairro);
            $("#detalhes_cadastro_municipio").html(model.enderecoCadastro.municipio);
            $("#detalhes_cadastro_uf").html(model.enderecoCadastro.UF);
            $("#detalhes_cadastro_cep").html(model.enderecoCadastro.cep);
            $("#detalhes_cadastro_telefone1").html(model.enderecoCadastro.telefone1);
            $("#detalhes_cadastro_telefone2").html(model.enderecoCadastro.telefone2);
            $("#detalhes_cadastro_email").html(model.enderecoCadastro.email);

            $("#detalhes_cobranca_logradouro").html(model.enderecoCobranca.logradouro);
            $("#detalhes_cobranca_numero").html(model.enderecoCobranca.numero);
            $("#detalhes_cobranca_complemento").html(model.enderecoCobranca.complemento);
            $("#detalhes_cobranca_bairro").html(model.enderecoCobranca.bairro);
            $("#detalhes_cobranca_municipio").html(model.enderecoCobranca.municipio);
            $("#detalhes_cobranca_uf").html(model.enderecoCobranca.UF);
            $("#detalhes_cobranca_cep").html(model.enderecoCobranca.cep);
            $("#detalhes_cobranca_telefone1").html(model.enderecoCobranca.telefone1);
            $("#detalhes_cobranca_telefone2").html(model.enderecoCobranca.telefone2);
            $("#detalhes_cobranca_email").html(model.enderecoCobranca.email);

            $("#detalhes_entrega_logradouro").html(model.enderecoEntrega.logradouro);
            $("#detalhes_entrega_numero").html(model.enderecoEntrega.numero);
            $("#detalhes_entrega_complemento").html(model.enderecoEntrega.complemento);
            $("#detalhes_entrega_bairro").html(model.enderecoEntrega.bairro);
            $("#detalhes_entrega_municipio").html(model.enderecoEntrega.municipio);
            $("#detalhes_entrega_uf").html(model.enderecoEntrega.UF);
            $("#detalhes_entrega_cep").html(model.enderecoEntrega.cep);
            $("#detalhes_entrega_telefone1").html(model.enderecoEntrega.telefone1);
            $("#detalhes_entrega_telefone2").html(model.enderecoEntrega.telefone2);
            $("#detalhes_entrega_email").html(model.enderecoEntrega.email);


        }

    });
};