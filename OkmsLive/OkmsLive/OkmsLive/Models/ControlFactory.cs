using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OkmsLive.Models
{
    public class ControlFactory
    {

        public Control CreateCourseControl()
        {

            /*
             * 创建如下样式的control
             * <Border Background="#F7F7F7" Height="50" Margin="0,4,0,4" Width="660" BorderThickness="1" CornerRadius="1" BorderBrush="#FFF7F7F7" Cursor="Hand">
                                <StackPanel Orientation="Horizontal">
                                    <Image Width="24" Height="24" Stretch="Fill"  Source="/Resources/arrowGray.png"/>
                                    <Label VerticalContentAlignment="Center" Foreground="#FF333333" FontSize="14">OKMS需求研讨</Label>
                                    <Label VerticalContentAlignment="Center" Foreground="#FF666666" FontSize="14">（15:00-16:00）</Label>
                                    <StackPanel Orientation="Horizontal" FlowDirection="RightToLeft" HorizontalAlignment="Stretch" Width="400">
                                        <Label VerticalContentAlignment="Center" Foreground="#FF666666" FontSize="14">即将开始</Label>
                                    </StackPanel>
                                </StackPanel>
                            </Border>
             */

            var border = new Border();
            //border.Background = new SolidBrush(Color.FromArgb(247, 247, 247));

            return null;
        }


    }
}
