using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MindFusion.Charting;

namespace DateTimeSeries
{
	//A custom DateTime series class that supports adding values dynamically and 
	//specifying the interval of time stamps.
	class MyDateTimeSeries : Series
	{
		/// <summary>
		/// Initializes a new instance of the custom TimeSeries class.
		/// </summary>
		/// <param name="start">The first DateTime values of the series. The rest of the values will be added automatically.</param>
		/// <param name="minDate">The first visible date at the X-axis.</param>
		/// <param name="maxDate">The last visible </param>
		/// <param name="values"></param>
		public MyDateTimeSeries(DateTime start, DateTime minDate, DateTime maxDate)
		{
			this.start = start;	

			this.minDate = minDate;
			this.maxDate = maxDate;

			dateTimeFormat = DateTimeFormat.ShortTime;			
			customDateTimeFormat = "";
			labelInterval = 10;

			dates = new List<long>();
			values = new List<double>();
		}

		/// <summary>
		/// Gets the value at the specified index from the specified dimension
		/// </summary>
		/// <param name="index">The value index</param>
		/// <param name="dimension">The dimension</param>
		/// <returns></returns>
		public double GetValue(int index, int dimension)
		{
			if (dimension == 0)
			{
				if (index < dates.Count && index >= 0)
				{
					long currValue = dates[index];

					var p = (currValue - (double)minDate.Ticks) / ((double)maxDate.Ticks - (double)minDate.Ticks);

					return minValue + ((maxValue - minValue) * p);
				}
			}

			if (dimension == 1)
				return values[index];

			return 0;
		}

		/// <summary>
		/// Adds the specified value at the end of the
		/// data list.
		/// </summary>
		/// <param name="value"></param>
		public void addValue( double value)
		{
			this.values.Add(value);
			long currTime = DateTime.Now.Ticks;
			dates.Add(currTime);	
		}

		/// <summary>
		/// Gets the specified kind of label at the specified index.
		/// </summary>
		/// <param name="index">The index of the label</param>
		/// <param name="kind">The label kind.</param>
		/// <returns></returns>
		public string GetLabel(int index, LabelKinds kind)
		{
			if (kind == LabelKinds.XAxisLabel)
			{

				if (index < values.Count && index % labelInterval == 0)
				{
					DateTime dateTime = new DateTime(dates[index]);

					SortedList dateTimeFormats = new SortedList(9);
					dateTimeFormats.Add("d", DateTimeFormat.ShortDate);
					dateTimeFormats.Add("D", DateTimeFormat.LongDate);
					dateTimeFormats.Add("t", DateTimeFormat.ShortTime);
					dateTimeFormats.Add("T", DateTimeFormat.LongTime);
					dateTimeFormats.Add("M", DateTimeFormat.MonthDateTime);
					dateTimeFormats.Add("Y", DateTimeFormat.YearDateTime);
					dateTimeFormats.Add("f", DateTimeFormat.FullDateTime);
					dateTimeFormats.Add("*", DateTimeFormat.CustomDateTime);
					dateTimeFormats.Add("", DateTimeFormat.None);

					string format = customDateTimeFormat;

					if (dateTimeFormat != DateTimeFormat.None && 
						dateTimeFormat != DateTimeFormat.CustomDateTime)
					{
						int fIndex = dateTimeFormats.IndexOfValue(dateTimeFormat);
						format = dateTimeFormats.GetKey(fIndex).ToString();
						
					}

					return dateTime.ToString(format);
				}
				
			}
			
			return string.Empty;
		}

		public bool IsEmphasized(int index)
		{
			return false;
		}

		/// <summary>
		/// The series supports labels at the X-axis
		/// </summary>
		public LabelKinds SupportedLabels
		{
			get { return labelKinds; }
			set { labelKinds = value; }
		}

		/// <summary>
		/// The first dimension is sorted
		/// </summary>
		/// <param name="dimension"></param>
		/// <returns></returns>
		public bool IsSorted(int dimension)
		{
			return dimension == 0;
		}
		
		/// <summary>
		/// There are two dimensions
		/// </summary>
		public int Dimensions
		{
			get { return 2; }
		}

		/// <summary>
		/// The size of the series is equal to the 
		/// count of the valeus.
		/// </summary>
		public int Size
		{
			get { return values.Count; }
		}

		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the coordinate corresponding to MinDate.
		/// </summary>
		public double MinValue
		{
			get { return minValue; }
			set
			{
				if (minValue == value)
					return;

				minValue = value;
				OnDataChanged();
			}
		}

		/// <summary>
		/// Gets or sets the coordinate corresponding to MaxDate.
		/// </summary>
		public double MaxValue
		{
			get { return maxValue; }
			set
			{
				if (maxValue == value)
					return;

				maxValue = value;
				OnDataChanged();
			}
		}


		/// <summary>
		/// Gets or sets the start of the time range on the axis.
		/// </summary>
		public DateTime MinDate
		{
			get { return minDate; }
			set
			{
				if (minDate == value)
					return;

				minDate = value;
				OnDataChanged();
			}
		}

		/// <summary>
		/// Gets or sets the end of the time range on the axis.
		/// </summary>
		public DateTime MaxDate
		{
			get { return maxDate; }
			set
			{
				if (maxDate == value)
					return;

				maxDate = value;
				OnDataChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating how to format DateTime values as labels.
		/// </summary>
		public DateTimeFormat DateTimeFormat
		{
			get { return dateTimeFormat; }
			set
			{
				if (dateTimeFormat == value)
					return;

				dateTimeFormat = value;
				OnDataChanged();
			}
		}

		/// <summary>
		/// Gets or sets how many values will be skipped before the 
		/// timestamp of an entry is rendered as a label at the X-axis.
		/// </summary>
		public int LabelInterval
		{
			get { return labelInterval; }
			set
			{
				if (labelInterval == value)
					return;

				labelInterval = value;
				OnDataChanged();
			}
		}

		/// <summary>
		/// Gets or sets a custom format string for DateTime labels.
		/// </summary>
		public string CustomDateTimeFormat
		{
			get { return customDateTimeFormat; }
			set
			{
				if (customDateTimeFormat == value)
					return;

				customDateTimeFormat = value;
				OnDataChanged();
			}
		}

		public event EventHandler DataChanged;

		/// <summary>
		/// Raises the DataChanged event.
		/// </summary>
		protected virtual void OnDataChanged()
		{
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}


		DateTime start;
		List<double> values;
		List<long> dates;
		private DateTime minDate;
		private DateTime maxDate;

		//the numerical values of the axis that should be mapped
		//to the minDate and maxDate. Could be public properties.
		private double minValue = 0;
		private double maxValue = 1;

		private int labelInterval;
		private DateTimeFormat dateTimeFormat;
		private string customDateTimeFormat;
		private LabelKinds labelKinds;
	}
}
