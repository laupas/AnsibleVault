# A simple Ansible Vault implementation for .net core

![.NET Core](https://github.com/laupas/AnsibleVault/workflows/.NET%20Core/badge.svg?branch=master)

## Encode
```
var input = "$ANSIBLE_VAULT;1.1;AES256" + Environment.NewLine +
            "33643238353032653563323633343066643261306333613130316266323135663864303465333063" + Environment.NewLine +
            "3030393132643338353237366462346634666566376262610a323838616631383065323030326565" + Environment.NewLine +
            "32353839323366323139303232343032366236373064613933663062663830616330353631646462" + Environment.NewLine +
            "3738643964393831650a383261633135376166626437356234643764363537363533393637343164" + Environment.NewLine +
            "64643231323565396135353962633966623361343030623464633436666432353462";

IAnsibleVault ansibleVault = new AnsibleVault()
var value = ansibleVault.Decode("1234", input);
```

## Decode
```
IAnsibleVault ansibleVault = new AnsibleVault()
var vaultValue = ansibleVault.Encode("1234", "abcd");
```


