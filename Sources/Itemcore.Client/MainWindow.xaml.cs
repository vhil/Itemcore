﻿using Itemcore.Client.ViewModels;
using Itemcore.Logging;

namespace Itemcore.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow(ILoggingService loggingService, MainWindowViewModel viewModel)
			: base(loggingService, viewModel)
		{
			InitializeComponent();
		}
	}
}
