﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FluentUI
{
    public static class FluentHelper
    {
        #region TextColumnStyle

        /// <summary>
        /// TextColumnStyle Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty TextColumnStyleProperty =
            DependencyProperty.RegisterAttached("TextColumnStyle", typeof(Style), typeof(FluentHelper),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnTextColumnStyleChanged)));

        /// <summary>
        /// Gets the TextColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static Style GetTextColumnStyle(DependencyObject d)
        {
            return (Style)d.GetValue(TextColumnStyleProperty);
        }

        /// <summary>
        /// Sets the TextColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetTextColumnStyle(DependencyObject d, Style value)
        {
            d.SetValue(TextColumnStyleProperty, value);
        }

        /// <summary>
        /// Handles changes to the TextColumnStyle property.
        /// </summary>
        private static void OnTextColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGrid grid))
                throw new InvalidOperationException("TextColumnStyle property works only on DataGrid");

            if (e.OldValue == null && e.NewValue != DependencyProperty.UnsetValue)
            {
                foreach (var textColumn in grid.Columns
                    .OfType<DataGridTextColumn>()
                    //.Where(_ => _.EditingElementStyle == null)
                    )
                {
                    textColumn.EditingElementStyle = GetTextColumnStyle(grid);
                }

                grid.Columns.CollectionChanged += (s, eventArgs) =>
                {
                    if (eventArgs.NewItems != null)
                    {
                        foreach (var textColumn in eventArgs.NewItems
                            .OfType<DataGridTextColumn>()
                            //.Where(_ => _.EditingElementStyle == null)
                            )
                        {
                            textColumn.EditingElementStyle = GetTextColumnStyle((DataGrid)s);
                        }
                    }
                };
            }
        }


        #endregion

        #region ComboBoxColumnStyle

        /// <summary>
        /// ComboBoxColumnStyle Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ComboBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("ComboBoxColumnStyle", typeof(Style), typeof(FluentHelper),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnComboBoxColumnStyleChanged)));

        /// <summary>
        /// Gets the ComboBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static Style GetComboBoxColumnStyle(DependencyObject d)
        {
            return (Style)d.GetValue(ComboBoxColumnStyleProperty);
        }

        /// <summary>
        /// Sets the ComboBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetComboBoxColumnStyle(DependencyObject d, Style value)
        {
            d.SetValue(ComboBoxColumnStyleProperty, value);
        }

        /// <summary>
        /// Handles changes to the ComboBoxColumnStyle property.
        /// </summary>
        private static void OnComboBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGrid grid))
                throw new InvalidOperationException("ComboBoxColumnStyle property works only on DataGrid");

            if (e.OldValue == null && e.NewValue != DependencyProperty.UnsetValue)
            {
                foreach (var textColumn in grid.Columns
                    .OfType<DataGridComboBoxColumn>()
                    //.Where(_ => _.EditingElementStyle == null)
                    )
                {
                    textColumn.EditingElementStyle = GetComboBoxColumnStyle(grid);
                }

                grid.Columns.CollectionChanged += (s, eventArgs) =>
                {
                    if (eventArgs.NewItems != null)
                    {
                        foreach (var comboBoxColumn in eventArgs.NewItems
                            .OfType<DataGridComboBoxColumn>()
                            //.Where(_ => _.EditingElementStyle == null)
                            )
                        {
                            comboBoxColumn.EditingElementStyle = GetComboBoxColumnStyle((DataGrid)s);
                        }
                    }

                };
            }
        }

        #endregion


        #region CheckBoxColumnStyle

        /// <summary>
        /// CheckBoxColumnStyle Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CheckBoxColumnStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnStyle", typeof(Style), typeof(FluentHelper),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnCheckBoxColumnStyleChanged)));

        /// <summary>
        /// Gets the CheckBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static Style GetCheckBoxColumnStyle(DependencyObject d)
        {
            return (Style)d.GetValue(CheckBoxColumnStyleProperty);
        }

        /// <summary>
        /// Sets the CheckBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetCheckBoxColumnStyle(DependencyObject d, Style value)
        {
            d.SetValue(CheckBoxColumnStyleProperty, value);
        }

        /// <summary>
        /// CheckBoxColumnEditingStyle Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty CheckBoxColumnEditingStyleProperty =
            DependencyProperty.RegisterAttached("CheckBoxColumnEditingStyle", typeof(Style), typeof(FluentHelper),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    new PropertyChangedCallback(OnCheckBoxColumnStyleChanged)));

        /// <summary>
        /// Gets the CheckBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static Style GetCheckBoxColumnEditingStyle(DependencyObject d)
        {
            return (Style)d.GetValue(CheckBoxColumnEditingStyleProperty);
        }

        /// <summary>
        /// Sets the CheckBoxColumnStyle property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetCheckBoxColumnEditingStyle(DependencyObject d, Style value)
        {
            d.SetValue(CheckBoxColumnEditingStyleProperty, value);
        }

        /// <summary>
        /// Handles changes to the CheckBoxColumnStyle property.
        /// </summary>
        private static void OnCheckBoxColumnStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGrid grid))
                throw new InvalidOperationException("CheckBoxColumnStyle or CheckBoxColumnEditingStyle property works only on DataGrid");

            if (e.OldValue == null && e.NewValue != DependencyProperty.UnsetValue)
            {
                foreach (var textColumn in grid.Columns
                    .OfType<DataGridCheckBoxColumn>()
                    //.Where(_ => _.EditingElementStyle == null)
                    )
                {
                    textColumn.ElementStyle =
                            GetCheckBoxColumnStyle(grid);
                    textColumn.EditingElementStyle =
                            GetCheckBoxColumnEditingStyle(grid);
                }

                grid.Columns.CollectionChanged += (s, eventArgs) =>
                {
                    if (eventArgs.NewItems != null)
                    {
                        foreach (var CheckBoxColumn in eventArgs.NewItems
                            .OfType<DataGridCheckBoxColumn>()
                            //.Where(_ => _.EditingElementStyle == null)
                            )
                        {
                            CheckBoxColumn.EditingElementStyle = GetCheckBoxColumnStyle((DataGrid)s);
                        }
                    }

                };
            }
        }

        #endregion
    }
}
