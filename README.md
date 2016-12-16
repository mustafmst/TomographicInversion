
#Opis algorytmu mrówkowego oraz opcje potrzebne do wykonywania różnych działań

###[Faza przemieszczania] schemat działania mrówki:
* wygenerowanie nowego rozwiazania(losowanie pozycji wektora do zmiany oraz losowe podniesienie lub obniżenie o zadaną wartość)
* sprawdzenie czy wygenerowane rozwiązanie istnieje(jesli tak: połączenie go z aktualnym nodem; jesli nie: dodanie go do puli rozwiazań i połaczenie z aktualnym nodem)
* losowy wybór następnego node'a z dostępnych(zapach jako wagi)
* przejście na nowe rozwiązanie

###[Faza zostawiania zapachu] schemat zostawiania zapachu przez mrówkę:
* wyliczenie błędu procentowego
* zostawienie w nodzie zapachu(dodanie 1 punktu na każde 10% poniżej 100%[0-10% - 10pkt, 10-20 - 9pkt itd)

###[Faza wyparowania] 
pod koniec iteracji obniżenie wartości zapachu o określoną liczbę(nie może spaść poniżej 1)

###Iteracja algorytmu:
* wszystkie mrówki wykonują przemieszczanie się
* wszystkie mrówki pozostawiają zapach
* wyparowanie zapachu

Po określonej liczbie iteracji wybierane są węzły z największą liczbą mrówek a z nich ten z najmniejszym średnim błędem

###Opcje w ustawieniach:
* wartości początkowe(wartość srednia lub losowe wartości w przedziału)
* ilość iteracji
* ilość osobników
* nazwa pliku z danymi wynikowymi
* wartość do inkrementacji/dekrementacji wartości zmienianej
* współczynnik parowania zapachu
