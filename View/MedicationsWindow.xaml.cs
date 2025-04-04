﻿using PharmacyApp.Models;
using PharmacyApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PharmacyApp.Repositories;
using PharmacyApp.ViewModel;

namespace PharmacyApp.View
{
    /// <summary>
    /// Логика взаимодействия для MedicationsWindow.xaml
    /// </summary>
    public partial class MedicationsWindow : Window
    {
        public MedicationsWindow()
        {
            InitializeComponent();
            var context = new PharmacyDbContext();
            IMedicationsRepository medicationsRepository = new MedicationsRepository(context);
            DataContext = new MedicationsViewModel(medicationsRepository);
        }
    }
}
