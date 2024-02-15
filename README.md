Instalação do RabbitMQ usando Chocolatey
Este guia fornecerá instruções passo a passo sobre como instalar o RabbitMQ no Windows usando o Chocolatey.

Pré-requisitos
Antes de começar, certifique-se de que você tenha o Chocolatey instalado. Se você ainda não tiver o Chocolatey instalado, siga as instruções abaixo.

Instalação do Chocolatey
Abra o PowerShell como administrador.

Execute o seguinte comando para instalar o Chocolatey:
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

Após a instalação ser concluída, verifique se o Chocolatey está funcionando corretamente digitando o seguinte comando:
choco -?

Instalação do RabbitMQ
Agora que o Chocolatey está instalado, podemos prosseguir com a instalação do RabbitMQ.

Abra o PowerShell como administrador.

Execute o seguinte comando para instalar o RabbitMQ:
choco install rabbitmq

Após a instalação ser concluída, o RabbitMQ estará pronto para uso. Você pode iniciar o servidor RabbitMQ executando o seguinte comando:
rabbitmq-server start

Para acessar o painel de administração do RabbitMQ, abra seu navegador da web e vá para http://localhost:15672. O nome de usuário padrão é 'guest' e a senha padrão é 'guest'.

