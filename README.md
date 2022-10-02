# T120B165- Įrašų parduotuvė<br/>

## 1. Sprendžiamo uždavinio aprašymas<br/>
### 1.1. Sistemos paskirtis<br/>

**Projekto tikslas –** paspartinti bei pagerinti muzikinių įrašų pardavimo procesą.<br/>

**Veikimo principas –** šį projektą sudarys du komponentai: internetine aplikacija taip pat bus pasitelkiama aplikacijų programavimo sąsaja.<br/>

**Naudojimas –** Pardavėjas norėdamas naudotis šia aplikacija prisiregistruos prie jos, galės sudaryti muzikinių įrašų skelbimus, ikelti paveiksliukus, užpildyti reikiama informaciją apie atlikėją, albumą bei kita aktualią informaciją, sudaryti užsakymus. Sudarytus skelbimus prisijungę naudotojai galės komentuoti, reitinguoti, pridėti į krepšelį ir nusipirkti. Administratorius tvirtina arba atšaukia naujas pardavėjų registracijas, peržiūri sudarytus skelbimus prieš paskelbimą (supildyta reikalinga informacija, skelbimas nėra klaidingas, apgavingas ar panašiai).<br/>

### 1.2. Funkciniai reikalavimai

**Svečias projekte galės:**
1.	Peržiūrėti pradinį puslapį.
2.	Registruotis į internetinį puslapį.
3.	Prisijungti prie internetinio puslapio.

**Registruotas naudotojas (pardavėjas) galės:**
1.	Prisijungti prie aplikacijos.
2.	Atsijungti nuo aplikacijos.
3.	Sukurti skelbimą:
4.	Įkelti paveiksliuką ar paveiksliukus.
5.	Pridėti muzikinio įrašo aprašymą.
6.	Pridėti kitą aktualią informaciją.
7.	Paskelbti sukurtą skelbimą.
8.	Peržiūrėti kitų pardavėjų sukurtus skelbimus.
9.	Komentuoti kitų pardavėjų sukurtus skelbimus.
10.	Reitinguoti kitų pardavėjų sukurtus skelbimus.
11.	Nusipirkti muzikinį įrašą/įrašus.

**Administratorius galės:**
1.	Patvirtinti pardavėjo registraciją.
2.	Patvirtinti pardavėjų sukurtus skelbimus.
3.	Šalinti pasirinktus pardavėjus.
4.	Šalinti netinkamus skelbimus.

## 2. Sistemos architektūra

**Sistema sudarys:**
•	Kliento pusė – Vue.js<br/>
•	Serverio pusė – .NET Core, duomenų bazė – MS SQL Server.<br/>
Žemiau pateiktame paveiksliuke matome sistemos diegimo diagrama. Sistemos talpinimui bus pasirintkas Azure/AWS serveris. Kiekviena sistemos dalis bus sudiegta tam pačiame serveryje. Internetinę aplikaciją naudotojas galės pasiekti naudodamas HTTP protokolą (interneto naršyklę). Šioje sistemoje taip pat naudosime savo sukurtą įrašų parduotuvės aplikacijų programavimo sąsają. Saugoti įrašus naudosime MS SQL serverį.<br/>

![deployment](https://i.imgur.com/SIMQNJh.jpg)



