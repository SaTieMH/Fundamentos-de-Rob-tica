using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SVG_IRB1600_Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Required parameter missing");
                return;
            }
            StreamReader streamReader;
            StreamWriter streamWriter;
            String line;
            String[] regex_patterns = new String[7];
            String program_output1 = "";
            String program_output2 = "";
            String program_output3 = "";
            String svg_output1 = "";
            String svg_output2 = "";
            Console.WriteLine("Reading config...");
            if (!Directory.Exists("Config"))
            {
                Directory.CreateDirectory("Config");
            }
            if (!File.Exists("Config/Regex.txt"))
            {
                streamWriter = new StreamWriter("Config/Regex.txt");
                streamWriter.WriteLine("viewBox=\"[0-9]+ [0-9]+ ([0-9]+) ([0-9]+)\"");
                streamWriter.WriteLine("<polyline .*? points=\"(.*?)\"[ ]*\\/>");
                streamWriter.WriteLine("([0-9]+),([0-9]+)");
                streamWriter.WriteLine("<line .*? x1=\"([0-9]+)\" y1=\"([0-9]+)\" x2=\"([0-9]+)\" y2= \"([0-9]+)\" \\/>");
                streamWriter.WriteLine("<path .*? d=\"(.*?)\"\\/>");
                streamWriter.WriteLine("([a-zA-Z])((?:[0-9-]+[ \tab,-]*)+)");
                streamWriter.WriteLine("MoveL[ \t]*Offs[ \t]*\\(p0,[ \t]*([0-9\\.]+)[ \t]*,[ \t]*([0-9\\.]+)[ \t]*,[ \t]*([0-9\\.]+)[ \t]*\\),[ \t]*v[0-9]+[ \t]*,[ \t]*z[0-9]+[ \t]*,[ \t]*tool0[ \t]*;");
                streamWriter.Close();
            }
            if (!File.Exists("Config/Program_Output1.txt"))
            {
                streamWriter = new StreamWriter("Config/Program_Output1.txt");
                streamWriter.WriteLine("MODULE MainModule");
                streamWriter.WriteLine("	CONST robtarget p0:=[[600.00,-297.00,500.00],[3.48237E-08,-0.707107,-0.707107,1.17632E-08],[-1,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];");
                streamWriter.WriteLine("	PROC main()");
                streamWriter.WriteLine("	MoveJ	p0,	v10,	z10,	tool0;");
                streamWriter.WriteLine("");
                streamWriter.Close();
            }
            if (!File.Exists("Config/Program_Output2.txt"))
            {
                streamWriter = new StreamWriter("Config/Program_Output2.txt");
                streamWriter.WriteLine("	MoveL	Offs	(p0,	<x>,	<y>,	<z>),	v10,	z10,	tool0;");
                streamWriter.Close();
            }
            if (!File.Exists("Config/Program_Output3.txt"))
            {
                streamWriter = new StreamWriter("Config/Program_Output3.txt");
                streamWriter.WriteLine("	MoveL	Offs	(p0,	0,	0,	20),	v10,	z10,	tool0;");
                streamWriter.WriteLine("	MoveL	Offs	(p0,	0,	0,	0),	v10,	z10,	tool0;");
                streamWriter.WriteLine("");
                streamWriter.WriteLine("	ENDPROC");
                streamWriter.WriteLine("ENDMODULE");
                streamWriter.Close();
            }
            if (!File.Exists("Config/SVG_Output1.txt"))
            {
                streamWriter = new StreamWriter("Config/SVG_Output1.txt");
                streamWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                streamWriter.WriteLine("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
                streamWriter.WriteLine("<svg xmlns=\"http://www.w3.org/2000/svg\" xml:space=\"preserve\" width=\"<width>mm\" height=\"<height>mm\" version=\"1.1\" style=\"shape-rendering:geometricPrecision; text-rendering:geometricPrecision; image-rendering:optimizeQuality; fill-rule:evenodd; clip-rule:evenodd\" viewBox=\"0 0 <viewBoxWidth> <viewBoxHeight>\" xmlns:xlink=\"http://www.w3.org/1999/xlink\">");
                streamWriter.WriteLine(" <defs>");
                streamWriter.WriteLine("  <style type=\"text/css\">");
                streamWriter.WriteLine("   <![CDATA[");
                streamWriter.WriteLine("    .str0 {stroke:#000000;stroke-width:100;stroke-linecap:round;stroke-linejoin:round}");
                streamWriter.WriteLine("    .fil0 {fill:none}");
                streamWriter.WriteLine("   ]]>");
                streamWriter.WriteLine("  </style>");
                streamWriter.WriteLine(" </defs>");
                streamWriter.Close();
            }
            if (!File.Exists("Config/SVG_Output2.txt"))
            {
                streamWriter = new StreamWriter("Config/SVG_Output2.txt");
                streamWriter.WriteLine("</svg>");
                streamWriter.Close();
            }
            streamReader = new StreamReader("Config/Regex.txt");
            for (int i = 0; i < 7; i++)
            {
                regex_patterns[i] = streamReader.ReadLine();
            }
            streamReader.Close();
            streamReader = new StreamReader("Config/Program_Output1.txt");
            Boolean start = true;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    program_output1 += Environment.NewLine;
                }
                program_output1 += line;
            }
            streamReader.Close();
            streamReader = new StreamReader("Config/Program_Output2.txt");
            program_output2 = streamReader.ReadLine();
            streamReader.Close();
            streamReader = new StreamReader("Config/Program_Output3.txt");
            start = true;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    program_output3 += Environment.NewLine;
                }
                program_output3 += line;
            }
            streamReader.Close();
            streamReader = new StreamReader("Config/SVG_Output1.txt");
            start = true;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    svg_output1 += Environment.NewLine;
                }
                svg_output1 += line;
            }
            streamReader.Close();
            streamReader = new StreamReader("Config/SVG_Output2.txt");
            start = true;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (start)
                {
                    start = false;
                }
                else
                {
                    svg_output2 += Environment.NewLine;
                }
                svg_output2 += line;
            }
            streamReader.Close();
            Console.WriteLine("Converting...");
            streamReader = new StreamReader(args[0]);
            streamWriter = new StreamWriter(args[1]);
            if (args.Length == 2)
            {
                int width = 0;
                int height = 0;
                streamWriter.WriteLine(program_output1);
                float lastX = 0;
                float lastY = 0;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Regex regex = new Regex(regex_patterns[0]);
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        width = Convert.ToInt32(match.Groups[1].Value);
                        height = Convert.ToInt32(match.Groups[2].Value);
                    }
                    regex = new Regex(regex_patterns[1]);
                    match = regex.Match(line);
                    if (match.Success)
                    {
                        Boolean first = true;
                        Regex regex2 = new Regex(regex_patterns[2]);
                        Match match2 = regex2.Match(match.Groups[1].Value);
                        while (match2.Success)
                        {
                            float x = Convert.ToInt32(match2.Groups[1].Value) / 100f;
                            float y = (height - Convert.ToInt32(match2.Groups[2].Value)) / 100f;
                            if (first)
                            {
                                streamWriter.WriteLine(getProgramLine(lastX, lastY, 20, program_output2));
                                streamWriter.WriteLine(getProgramLine(x, y, 20, program_output2));
                                first = false;
                            }
                            streamWriter.WriteLine(getProgramLine(x, y, 0, program_output2));
                            lastX = x;
                            lastY = y;
                            match2 = match2.NextMatch();
                        }
                    }
                    regex = new Regex(regex_patterns[3]);
                    match = regex.Match(line);
                    if (match.Success)
                    {
                        float x1 = Convert.ToInt32(match.Groups[1].Value) / 100f;
                        float y1 = (height - Convert.ToInt32(match.Groups[2].Value)) / 100f;
                        float x2 = Convert.ToInt32(match.Groups[3].Value) / 100f;
                        float y2 = (height - Convert.ToInt32(match.Groups[4].Value)) / 100f;
                        streamWriter.WriteLine(getProgramLine(lastX, lastY, 20, program_output2));
                        streamWriter.WriteLine(getProgramLine(x1, y1, 20, program_output2));
                        streamWriter.WriteLine(getProgramLine(x1, y1, 0, program_output2));
                        streamWriter.WriteLine(getProgramLine(x2, y2, 0, program_output2));
                        lastX = x2;
                        lastY = y2;
                    }
                    regex = new Regex(regex_patterns[4]);
                    match = regex.Match(line);
                    if (match.Success)
                    {
                        Regex regex2 = new Regex(regex_patterns[5]);
                        Match match2 = regex2.Match(match.Groups[1].Value);
                        while (match2.Success)
                        {
                            String[] coords = match2.Groups[2].Value.Split(' ');
                            if (match2.Groups[1].Value == "M")
                            {
                                float x = Convert.ToInt32(coords[0]) / 100f;
                                float y = (height - Convert.ToInt32(coords[1])) / 100f;
                                streamWriter.WriteLine(getProgramLine(lastX, lastY, 20, program_output2));
                                streamWriter.WriteLine(getProgramLine(x, y, 20, program_output2));
                                streamWriter.WriteLine(getProgramLine(x, y, 0, program_output2));
                                lastX = x;
                                lastY = y;
                            }
                            else if (match2.Groups[1].Value == "m")
                            {
                                float x = lastX + (Convert.ToInt32(coords[0]) / 100f);
                                float y = height / 100 - ((height / 100 - lastY) + Convert.ToInt32(coords[1]) / 100f);
                                streamWriter.WriteLine(getProgramLine(lastX, lastY, 20, program_output2));
                                streamWriter.WriteLine(getProgramLine(x, y, 20, program_output2));
                                streamWriter.WriteLine(getProgramLine(x, y, 0, program_output2));
                                lastX = x;
                                lastY = y;
                            }
                            else if (match2.Groups[1].Value == "l")
                            {
                                int size = coords.Length / 2;
                                for (int i = 0; i < size; i++)
                                {
                                    float x = lastX + Convert.ToInt32(coords[i * 2 + 0]) / 100f;
                                    float y = height / 100 - ((height / 100 - lastY) + Convert.ToInt32(coords[i * 2 + 1]) / 100f);
                                    streamWriter.WriteLine(getProgramLine(x, y, 0, program_output2));
                                    lastX = x;
                                    lastY = y;
                                }
                            }
                            else if (match2.Groups[1].Value == "c")
                            {
                                int limit = coords.Length / 3;
                                for (int j = 0; j < limit; j++)
                                {
                                    double x0 = lastX;
                                    double y0 = lastY;
                                    String[] xy1 = coords[j * 3 + 0].Split(',');
                                    double x1 = Convert.ToDouble(xy1[0]) / 100 + lastX;
                                    double y1 = height / 100 - ((height / 100 - lastY) + Convert.ToDouble(xy1[1]) / 100);
                                    String[] xy2 = coords[j * 3 + 1].Split(',');
                                    double x2 = Convert.ToDouble(xy2[0]) / 100 + lastX;
                                    double y2 = height / 100 - ((height / 100 - lastY) + Convert.ToDouble(xy2[1]) / 100);
                                    String[] xy3 = coords[j * 3 + 2].Split(',');
                                    double x3 = Convert.ToDouble(xy3[0]) / 100 + lastX;
                                    double y3 = height / 100 - ((height / 100 - lastY) + Convert.ToDouble(xy3[1]) / 100);
                                    double[] bx = new double[11];
                                    double[] by = new double[11];
                                    for (int i = 0; i < 11; i++)
                                    {
                                        double t = i / 10f;
                                        bx[i] = Math.Pow(1 - t, 3) * x0 + 3 * t * Math.Pow(1 - t, 2) * x1 + 3 * Math.Pow(t, 2) * (1 - t) * x2 + Math.Pow(t, 3) * x3;
                                        by[i] = Math.Pow(1 - t, 3) * y0 + 3 * t * Math.Pow(1 - t, 2) * y1 + 3 * Math.Pow(t, 2) * (1 - t) * y2 + Math.Pow(t, 3) * y3;
                                    }
                                    for (int i = 0; i < 11; i++)
                                    {
                                        streamWriter.WriteLine(getProgramLine((float)bx[i], (float)by[i], 0, program_output2));
                                    }
                                    lastX = (float)x3;
                                    lastY = (float)y3;
                                }
                            }
                            match2 = match2.NextMatch();
                        }
                    }
                }
                streamWriter.WriteLine(getProgramLine(lastX, lastY, 20, program_output2));
                streamWriter.WriteLine(program_output3);
                streamReader.Close();
                streamWriter.Close();
            }
            else
            {
                String width = args[2];
                String height = args[3];
                String viewBoxWidth = Convert.ToString(Convert.ToInt32(width) * 100);
                String viewBoxHeight = Convert.ToString(Convert.ToInt32(height) * 100);
                svg_output1 = svg_output1.Replace("<width>", width).Replace("<height>", height).Replace("<viewBoxWidth>", viewBoxWidth).Replace("<viewBoxHeight>", viewBoxHeight);
                streamWriter.WriteLine(svg_output1);
                Regex regex = new Regex(regex_patterns[6]);
                Boolean newLine = true;
                Boolean first = true;
                while ((line = streamReader.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        if (Convert.ToDouble(match.Groups[3].Value) != 0)
                        {
                            newLine = true;
                        }
                        else
                        {
                            if (newLine)
                            {
                                if (first)
                                {
                                    first = false;
                                }
                                else
                                {
                                    streamWriter.WriteLine("\" />");
                                }
                                streamWriter.Write("  <polyline class=\"fil0 str0\" points=\"");
                            }
                            int x = Convert.ToInt32(Convert.ToDouble(match.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture) * 100);
                            int y = Convert.ToInt32(viewBoxHeight) - Convert.ToInt32(Convert.ToDouble(match.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture) * 100);
                            streamWriter.Write(x + "," + y + " ");
                            newLine = false;
                        }
                    }
                }
                streamWriter.WriteLine("\" />");
                streamWriter.WriteLine(svg_output2);
                streamReader.Close();
                streamWriter.Close();
            }
            Console.WriteLine("Finished");
        }

        static String getProgramLine(float x, float y, float z, String format)
        {
            String xs = x.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            String ys = y.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            String zs = z.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            String result = format.Replace("<x>", xs).Replace("<y>", ys).Replace("<z>", zs);
            return result;
        }
    }
}