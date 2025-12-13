📘 Projekti I-Terminal
📅 Data


07/11/2025

📚 Lënda

Inxhinieri Softi

👥 Anëtarët e Grupit

Danjel Rexhaj

Gresijola Filaj

Gentjan Nergjoni

Gerald Vreto

🚌 Përshkrimi i Projektit

I-Terminal është një sistem softuerik i projektuar për rezervimin dhe pagesën elektronike të biletave të autobusëve. Qëllimi kryesor i këtij projekti është të lehtësojë procesin e udhëtimit për përdoruesit duke ofruar një platformë të thjeshtë, të sigurt dhe efikase për:

zgjedhjen e destinacioneve,

shikimin e orareve të disponueshme,

rezervimin e vendeve,

dhe kryerjen e pagesave online.

Projekti është ndërtuar duke zbatuar parimet e Inxhinierisë së Softit, analizës së kërkesave dhe modelimit të sistemit përmes diagrameve UML.

🎯 Objektivat Kryesore

Digjitalizimi i procesit të rezervimit të biletave

Ulja e kohës dhe përpjekjes për përdoruesit

Sigurimi i pagesave elektronike

Menaxhim efikas i linjave dhe orareve nga admin-i

Strukturë e qartë dhe e mirëmbajtshme e sistemit

🧩 Funksionalitetet Kryesore
👤 Për Përdoruesin

Regjistrim dhe login në sistem

Shfletim i destinacioneve dhe orareve

Filtrim sipas datës dhe orës

Rezervim bilete

Pagesë online (Kartë / PayPal)

Shikim i historikut të rezervimeve

Marrje e konfirmimit me email pas pagesës

🛠️ Për Administratorin

Menaxhim i linjave dhe orareve

Menaxhim i udhëtimeve dhe autobusëve

Kontroll i rezervimeve

Verifikim i pagesave

Siguri dhe mbrojtje e të dhënave të përdoruesve

📋 Kërkesat e Sistemit
🔹 Kërkesa Funksionale

Regjistrim dhe autentifikim i përdoruesve

Rezervim dhe pagesë biletash

Ruajtje e të dhënave në databazë

Integrim me sistem pagesash

🔹 Kërkesa Jo-Funksionale

Siguri dhe privatësi (GDPR)

Dërgim email-i konfirmimi

Disponueshmëri dhe performancë

Mbështetje për customer service dhe AI në të ardhmen

Mundësi për zbritje dhe kredi për përdoruesit aktivë

🗂️ Arkitektura e Sistemit

Sistemi I-Terminal përdor një arkitekturë të kombinuar:

🔹 Client–Server

Përdoruesit dhe admin-i ndërveprojnë me serverin përmes internetit, duke dërguar kërkesa dhe duke marrë përgjigje në formë API.

🔹 Arkitekturë e Shtresëzuar (Three-Tier Architecture)

Presentation Layer – Ndërfaqja e përdoruesit

Business Logic Layer – Logjika kryesore e sistemit

Data Access Layer – Komunikimi me bazën e të dhënave

Kjo arkitekturë siguron:

shkallëzim,

mirëmbajtje të lehtë,

siguri të lartë,

ndarje të qartë të përgjegjësive.

🗄️ Dizajni i Bazës së të Dhënave

Sistemi përfshin tabela kryesore si:

User

Customer

Admin

Trip

Bus

Seat

Reservation

Ticket

Payment

Lidhjet mes klasave janë ndërtuar sipas Entity Relationship Diagram (ERD) për të garantuar integritetin dhe konsistencën e të dhënave.

📐 Diagramet UML

Use Case Diagram

Sequence Diagram (rezervim bilete & regjistrim user)

Entity Relationship Diagram (ERD)

Këto diagrame paraqesin qartë ndërveprimet mes përdoruesit dhe sistemit si dhe strukturën e brendshme të tij.

🔐 Siguria

Ruajtje e sigurt e kredencialeve

Validim i pagesave

Mbrojtje e të dhënave personale

Respektim i privatësisë së përdoruesve

🚀 Përfundim

Projekti I-Terminal ofron një zgjidhje moderne dhe të strukturuar për menaxhimin e rezervimeve të autobusëve. Arkitektura e përdorur, funksionalitetet e mirëpërcaktuara dhe integrimi me shërbime të jashtme e bëjnë sistemin të përshtatshëm për përdorim real dhe zhvillim të mëtejshëm në të ardhmen.

📌 Ky projekt është zhvilluar si punë grupi në kuadër të lëndës Inxhinieri Softi.
