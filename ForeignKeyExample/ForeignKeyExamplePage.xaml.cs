using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ForeignKeyExample
{
    public partial class ForeignKeyExamplePage : ContentPage
    {

        public ObservableCollection<Component> CList = new ObservableCollection<Component>();

        public ForeignKeyExamplePage()
        {
            InitializeComponent();
			var db = App.DB.enableFKey;

            //InsertAssetList();
            //InsertComponentList();

            var assetList = App.DB.GetAssetsAsync().Result;
            AssetListView.ItemsSource = assetList;

            var componentList = App.DB.GetComponentsAsync().Result;
            //CList = new ObservableCollection<Component>(componentList);
            ComponentListView.ItemsSource = componentList;

		}

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Asset;
            var componentList = App.DB.GetComponentsByAssetId(item).Result;
            ComponentListView.ItemsSource = componentList;
        }

        void InsertComponentList()
        {
            
            App.DB.InsertComponentAsync(new Component { Name = "Component 1", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 2", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 3", ParentAssetId = 1 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 4", ParentAssetId = 2 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 5", ParentAssetId = 2 });
            App.DB.InsertComponentAsync(new Component { Name = "Component 6", ParentAssetId = 3 });

        }

        void InsertAssetList()
        {
			App.DB.InsertAssetAsync(new Asset { Name = "Asset 1", Address = "", Comments = "", LocationDescription = "" });
			App.DB.InsertAssetAsync(new Asset { Name = "Asset 2", Address = "", Comments = "", LocationDescription = "" });
			App.DB.InsertAssetAsync(new Asset { Name = "Asset 3", Address = "", Comments = "", LocationDescription = "" });
			App.DB.InsertAssetAsync(new Asset { Name = "Asset 4", Address = "", Comments = "", LocationDescription = "" });

		}

        void RenameComponentList()
        {
            var componentList = App.DB.GetComponentsAsync().Result;
            foreach(Component component in componentList){
                component.Name = "Component " + component.Id;
                App.DB.UpdateComponentAsync(component);
            }

        }

        void RenameAssetList()
        {
            var assetList = App.DB.GetAssetsAsync().Result;

            assetList[2].Name = "Asset 3";
            assetList[3].Name = "Asset 4";

            App.DB.UpdateAssetAsync(assetList[2]);
            App.DB.UpdateAssetAsync(assetList[3]);

        }
    }
}
