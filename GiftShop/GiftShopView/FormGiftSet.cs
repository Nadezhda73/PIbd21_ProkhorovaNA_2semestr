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
using GiftShopBusinessLogic.ViewModels;
using Unity;

namespace GiftShopView
{
    public partial class FormGiftSet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IGiftSetLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> giftSetMaterials;
        public FormGiftSet(IGiftSetLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    GiftSetViewModel view = logic.Read(new GiftSetBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.GiftSetName;
                        textBoxPrice.Text = view.Price.ToString();
                        giftSetMaterials = view.GiftSetMaterials;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                giftSetMaterials = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (giftSetMaterials != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in giftSetMaterials)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
   MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormGiftSetMaterial>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (giftSetMaterials.ContainsKey(form.Id))
                {
                    giftSetMaterials[form.Id] = (form.MaterialName, form.Count);
                }
                else
                {
                    giftSetMaterials.Add(form.Id, (form.MaterialName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormGiftSetMaterial>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = giftSetMaterials[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    giftSetMaterials[form.Id] = (form.MaterialName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        giftSetMaterials.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (giftSetMaterials == null || giftSetMaterials.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new GiftSetBindingModel
                {
                    Id = id,
                    GiftSetName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    GiftSetMaterials = giftSetMaterials
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
