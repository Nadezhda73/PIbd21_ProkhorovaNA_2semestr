using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopBusinessLogic.BindingModels;
using GiftShopBusinessLogic.Interfaces;
using Unity;

namespace GiftShopView
{
    public partial class FormMaterial : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IMaterialLogic logic;
        private int? id;
        public FormMaterial(IMaterialLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormComponent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new MaterialBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.MaterialName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new MaterialBindingModel
                {
                    Id = id,
                    MaterialName = textBoxName.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
