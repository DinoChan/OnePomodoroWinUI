﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;


namespace OnePomodoro.Controls
{
    [ContentProperty(Name = nameof(Children))]
    public class ProgressToPointCollectionBridge : DependencyObject
    {
        public ProgressToPointCollectionBridge()
        {
            Children = new ObservableCollection<PointCollection>();
        }


        /// <summary>
        ///     获取或设置Points的值
        /// </summary>
        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }


        /// <summary>
        ///     获取或设置Progress的值
        /// </summary>
        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        /// <summary>
        ///     获取或设置Children的值
        /// </summary>
        public Collection<PointCollection> Children
        {
            get { return (Collection<PointCollection>)GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }


        protected virtual void OnProgressChanged(double oldValue, double newValue)
        {
            UpdatePoints();
        }


        protected virtual void OnChildrenChanged(Collection<PointCollection> oldValue, Collection<PointCollection> newValue)
        {
            var oldCollection = oldValue as INotifyCollectionChanged;
            if (oldCollection != null)
                oldCollection.CollectionChanged -= OnChildrenCollectionChanged;

            var newCollection = newValue as INotifyCollectionChanged;
            if (newCollection != null)
                newCollection.CollectionChanged += OnChildrenCollectionChanged;

            UpdatePoints();
        }

        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            if (Children == null || Children.Any() == false)
            {
                Points = null;
            }
            else if (Children.Count == 1)
            {
                var fromPoints = new PointCollection();
                for (var i = 0; i < Children[0].Count; i++)
                    fromPoints.Add(new Point(0, 0));
                var toPoints = Children[0];
                Points = GetCurrentPoints(fromPoints, toPoints, Progress);
            }
            else
            {
                var rangePerSection = 1d / (Children.Count - 1);
                var fromIndex = Math.Min(Children.Count - 2, Convert.ToInt32(Math.Floor(Progress / rangePerSection)));
                fromIndex = Math.Max(fromIndex, 0);
                var toIndex = fromIndex + 1;
                PointCollection fromPoints;
                if (fromIndex == toIndex)
                {
                    fromPoints = new PointCollection();
                    for (var i = 0; i < Children[0].Count; i++)
                        fromPoints.Add(new Point(0, 0));
                }
                else
                {
                    fromPoints = Children.ElementAt(fromIndex);
                }
                var toPoints = Children.ElementAt(toIndex);
                var percentage = (Progress / rangePerSection - fromIndex);

                Points = GetCurrentPoints(fromPoints, toPoints, percentage);
            }
        }

        private PointCollection GetCurrentPoints(PointCollection fromPoints, PointCollection toPoints, double percentage)
        {
            var points = new List<Point>();
            for (var i = 0;
                 i < Math.Min(fromPoints.Count, toPoints.Count);
                 i++)
            {
                var x = (1 - percentage) * fromPoints[i].X + percentage * toPoints[i].X;
                var y = (1 - percentage) * fromPoints[i].Y + percentage * toPoints[i].Y;

                points.Add(new Point(x, y));

            }
            var result = new PointCollection();
            foreach (var point in points.Distinct())
            {
                result.Add(point);
            }
            return result;
        }

        #region DependencyProperties

        /// <summary>
        ///     标识 Progress 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(ProgressToPointCollectionBridge), new PropertyMetadata(0d, OnProgressChanged));

        private static void OnProgressChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ProgressToPointCollectionBridge;
            var oldValue = (double)args.OldValue;
            var newValue = (double)args.NewValue;
            if (oldValue != newValue)
                target.OnProgressChanged(oldValue, newValue);
        }

        /// <summary>
        ///     标识 Children 依赖属性。
        /// </summary>
        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register("Children", typeof(Collection<PointCollection>), typeof(ProgressToPointCollectionBridge), new PropertyMetadata(null, OnChildrenChanged));

        private static void OnChildrenChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var target = obj as ProgressToPointCollectionBridge;
            var oldValue = (Collection<PointCollection>)args.OldValue;
            var newValue = (Collection<PointCollection>)args.NewValue;
            if (oldValue != newValue)
                target.OnChildrenChanged(oldValue, newValue);
        }

        /// <summary>
        ///     标识 Points 依赖属性。
        /// </summary>
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(ProgressToPointCollectionBridge), new PropertyMetadata(null));

        #endregion
    }

}
