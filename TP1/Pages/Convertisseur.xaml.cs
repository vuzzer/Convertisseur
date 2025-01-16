namespace TP1.Pages;

public partial class Convertisseur : ContentPage
{
    public Convertisseur()
    {
        InitializeComponent();
    }

    private async void OnNumberBtnClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;

        if (string.IsNullOrEmpty(valeurDepart.Text) && button.Text.Trim() == "0") {
            await DisplayAlert("Information", "La premiere valeur ne doit pas etre zéro", "OK");
        }

        if(!string.IsNullOrEmpty(valeurCible.Text))
        {
            return;
        }

        // Contrainte sur la longeur de la valeur de depart
        if (string.IsNullOrEmpty(valeurDepart.Text) || (!string.IsNullOrEmpty(valeurDepart.Text) && valeurDepart.Text.Trim().Length < 10)) {
            valeurDepart.Text += button.Text;
        }

    }

    private async void OnUnitBtnClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(valeurDepart.Text)) {
            await DisplayAlert("Information", "Veuillez saisir une valeur de départ", "OK");
            return;
        }


        // La conversion est faite lorsque le label valeur cible est vide
        if (string.IsNullOrEmpty(valeurCible.Text))
        {
            var button = (Button)sender;

            var uniteDepartEtCible = button.Text.Trim().Split("A");

            uniteDepart.Text = uniteDepartEtCible[0].Trim();

            uniteCible.Text = uniteDepartEtCible.Last().Trim();

            valeurCible.Text = Convertir(double.Parse(valeurDepart.Text), uniteDepart.Text, uniteCible.Text).ToString();
        }

    }

    private void effacerBtn_Clicked(object sender, EventArgs e)
    {
        valeurDepart.Text = "";
        valeurCible.Text = "";
        uniteDepart.Text = "";
        uniteCible.Text = "";
    }

    private double Convertir(double valeur, string uniteDepart, string uniteCible)
    {
        string key = $"{uniteDepart.ToUpper()} A {uniteCible.ToUpper()}";
        if (uniteDeConversion.ContainsKey(key))
        {
            return valeur * uniteDeConversion[key];
        }
        throw new ArgumentException("Conversion non supportée.");
    }

    private Dictionary<string, double> uniteDeConversion = new Dictionary<string, double>() {
            {"CM A POUCE", 0.393701 },
            {"M A PIED", 3.281 },
            {"G A ONCE", 0.035274 },
            {"KG A LBS", 2.20462 },
            {"LBS A KG", 0.453592 },
            {"ONCE A G", 28.3495 },
            {"PIED A M", 0.3048 },
            {"POUCE A CM", 2.54 }
        };


}
