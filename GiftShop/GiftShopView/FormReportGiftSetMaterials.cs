﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GiftShopBusinessLogic.BusinessLogics;
using Unity;
using GiftShopBusinessLogic.BindingModels;
using Microsoft.Reporting.WinForms;

namespace GiftShopView
{
    public partial class FormReportGiftSetMaterials : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        public FormReportGiftSetMaterials(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        
        private void buttonSaveToPdf_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveGiftSetMaterialsToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                        });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormReportGiftSetMaterials_Load(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetGiftSetMaterial();
                ReportDataSource source = new ReportDataSource("DataSetGiftSetMaterial", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
