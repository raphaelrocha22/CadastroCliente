$(document).ready(function () {

    $('#myform').validate({
        rules: {
            classe: { required: true },
            representante: { required: true },
            cnpj: { required: true },
            codun: { required: true },
            razaoSocial: { required: true },
            email: { email: true }
        },
        messages: {
            classe: { required: 'Campo obrigatorio' },
            representante: { required: 'Campo obrigatorio' },
            cnpj: { required: 'Campo obrigatorio' },
            codun: { required: 'Campo obrigatorio' },
            razaoSocial: { required: 'Campo obrigatorio' },
            email: { email: 'Ops, informe um email válido' },
        },
        submitHandler: function (form) {
            var model = {
                classe: $('#optClasse').val(),
                representante: $('#optRepresentante').val(),
                cnpj: $('#txtCnpj').val(),
                codun: $('#txtCodun').val(),
                razaoSocial: $('#txtRazaoSocial').val()
            };
            $.ajax({
                type: "POST",
                url: "/AreaRestrita/Cliente/Cadastro",
                data: model,
                success: function (data) {
                    alert("SIM");
                },
                Error: function (e) {
                    alert(e.message)
                }
            });
            return false;
        }
    });
    
    $('#btnConsultaCNPJ').click(function () {

        var cnpj = $('#txtCnpj').val().replace(/\.|-|[/]/g, '')

        var rgx = new RegExp("^[0-9]{14}$");
        if (rgx.test(cnpj)) {

            $.ajax({
                type: 'POST',
                url: '/AreaRestrita/Cliente/ConsultarCNPJ',
                data: model = {
                    NumeroCNPJ: cnpj
                },
                success:
                function (m) {
                    if (m.Status != "ERROR") {
                        $("#txtRazaoSocial").val(m.Nome);
                        $("#txtNomeFantasia").val(m.Fantasia);
                        $("#txtLogradouro").val(m.Logradouro);
                        $("#txtNumero").val(m.Numero);
                        $("#txtComplemento").val(m.Complemento);
                        $("#txtBairro").val(m.Bairro);
                        $("#txtMunicipio").val(m.Municipio);
                        $("#txtUF").val(m.Uf);
                        $("#txtCEP").val(m.Cep);
                        $("#txtEmail").val(m.Email);
                        $("#txtTelefone1").val(m.Telefone1);
                    }
                    else {
                        alert("CNPJ não encontrado!")
                    }
                },
                error: function (event) {
                    alert(event.message);
                }
            });
        }
        else {
            alert("CNPJ inválido, informe apenas caracteres numéricos com 14 caractéres")
        }
    });
});