using System;
using System.IO;
using System.Collections.Generic;
using LC;
namespace LC.DataTypes
{
	public class FileCode
	{
		public bool StreamOpened { get; private set; }
		public long Position 
		{ 
			get { if (StreamOpened) { return position; } else return -1L; }
			set { if (StreamOpened) { fs.Position = value; position = value; } }
		}
		public FileInfo Info { get; private set; }
		private FileStream fs;
		
        private bool nextTokenChar = false;
        private bool nextTokenLong = false;
        private bool clearToken = false;
        private bool clearNextToken = false;
		private bool clearPrewToken = false;
		private bool prewToken = false;
		private bool bytes = false;
		private int BytesCount = 0;
		private int bytesIndex = 0;
		private bool returnChar = false;
		private long position = 0L;

		public FileCode()
		{
			StreamOpened = false;
			Info = null;
			fs = null;
		}

		public void SetPath(string path) { Info = new(path); }
		public PosPoint GetPoint()
		{
			return new PosPoint(Position);
		}
		public void SetPoint(PosPoint p)
		{
			Position = p.Value;
		}
		public Token GetNextToken(ref LC.Lexer lexer, out bool end)
		{
			Start:
			Token t = new();
            LexerOut lo = new();
            int i;
            end = false;
			if (prewToken)
			{
				prewToken = false;
				return lexer.Prew;
			}
			if (bytes)
			{
			    if (bytesIndex < BytesCount)
			    {
			        Token tt = new()
			        {
			            Type = TokenType.Byte,
			            ValueByte = lexer.Bytes[bytesIndex],
			            IndexStartPos = lexer.PosIndex - (BytesCount - bytesIndex),
			            IndexStartLine = lexer.LineIndex
			        };
					bytesIndex++;
					return tt;
			    }
				else 
				{
					bytesIndex = 0;
					bytes = false;
				}
			}
			if (returnChar)
			{
				returnChar = false;
				clearToken = true;
				return lexer.Token;
			}
			if (clearPrewToken)
			{
				clearPrewToken = false;
				lexer.Prew = new();
			}
            if (clearToken)
            {
                lexer.Token = new();
                clearToken = false;
            }
            if (clearNextToken)
            {
                lexer.NextToken = new();
                clearNextToken = false;
            }
            if (nextTokenChar)
            {
                nextTokenChar = false;
                clearNextToken = true;
                return lexer.NextToken;
            }
            if (nextTokenLong)
            {
                nextTokenLong = false;
                lexer.Token = lexer.NextToken;
                lexer.NextToken = new();
            }
            while (true)
            {
                i = GetNextByte();
                //Console.WriteLine("[FileCode] " + i);
                if (i == -1) { end = true; return lexer.Token; }
                lo = lexer.MadeToken(i);
                if (lo.EndToken && !lo.NextToken && !lo.NextTokenLong)
                {
                    clearToken = true;
                    return lexer.Token;
                }
                if (lo.EndToken && lo.NextToken && !lo.NextTokenLong)
                {
                    nextTokenChar = true;
                    clearToken = true;
                    return lexer.Token;
                }
                if (lo.EndToken && lo.NextToken && lo.NextTokenLong)
                {
                    nextTokenLong = true;
                    clearToken = true;
                    return lexer.Token;
                }
				if (!lo.EndToken && lo.NewU && !lo.PrewToken)
				{
				    bytes = true;
				    BytesCount = lo.CountBytes;
				    goto Start;
				}
				if (lo.EndToken && lo.NewU && !lo.PrewToken)
				{
				    bytes = true;
				    returnChar = true;
				    BytesCount = lo.CountBytes;
				    goto Start;
				}
				if (!lo.EndToken && lo.NewU && lo.PrewToken)
				{
				    bytes = true;
				    prewToken = true;
				    BytesCount = lo.CountBytes;
				    goto Start;
				}
				if (lo.EndToken && lo.NewU && lo.PrewToken)
				{
				    prewToken = true;
				    bytes = true;
				    returnChar = true;
				    BytesCount = lo.CountBytes;
				    goto Start;
				}
            }
		}
		public void Create()
		{
			try 
			{
				fs = Info.Create();
				fs.Close();
			}
			catch (Exception e)
			{
				
				ErrorsHandler.Call(new()
				{
					Sender = Sender.FileCode,
					Type = TypeMassage.Error,
					LinesMassage = new()
					{
						Info.FullName + " FIleCode.StartReading()",
						e.ToString()
					}
				});
				if (fs != null) { fs.Close(); fs.Dispose(); StreamOpened = false; }
			}
		}
		public void StartStream()
		{
			try 
			{
				fs = Info.Open(FileMode.Open);
				fs.Seek(0L, SeekOrigin.Begin);
				position = 0L;
				StreamOpened = true;
			}
			catch (Exception e)
			{
				
				ErrorsHandler.Call(new()
				{
					Sender = Sender.FileCode,
					Type = TypeMassage.Error,
					LinesMassage = new()
					{
						Info.FullName + " FIleCode.StartReading()",
						e.ToString()
					}
				});
				if (fs != null) { fs.Close(); fs.Dispose(); StreamOpened = false; }
			}
		}
		public int GetNextByte()
		{
			try 
			{
				if (StreamOpened)
				{
					position++;
					return fs.ReadByte();
				}
				else
				{
					ErrorsHandler.Call(new()
					{
						Sender = Sender.FileCode,
						Type = TypeMassage.Error,
						LinesMassage = new()
						{
							Info.FullName + " FIleCode.GetNextByte()",
							Lang.Key("LC-Stream-not-started.")
						}
					});
				}
			}
			catch (Exception e)
			{
				ErrorsHandler.Call(new()
				{
					Sender = Sender.FileCode,
					Type = TypeMassage.Error,
					LinesMassage = new()
					{
						Info.FullName + " FIleCode.GetNextByte()",
						e.ToString()
					}
				});
				if (fs != null) { fs.Close(); fs.Dispose(); StreamOpened = false; }
			}
			return -1;
		}
		public void Write(byte[] bytes)
		{
			if (StreamOpened && fs != null)
			{
				try 
				{
					position++;
					fs.Write(bytes, 0, bytes.Length);
				}
				catch (Exception e)
				{
					ErrorsHandler.Call(new()
					{
						Sender = Sender.FileCode,
						Type = TypeMassage.Error,
						LinesMassage = new()
						{
							Info.FullName + " FIleCode.Write()",
							e.ToString()
						}
					});
					if (fs != null) { fs.Close(); fs.Dispose(); StreamOpened = false; }
				}
			}
		}
		public void CloseStream()
		{
			try 
			{
				if (StreamOpened) { StreamOpened = false; fs.Close(); }
			}
			catch (Exception e)
			{
				ErrorsHandler.Call(new()
				{
					Sender = Sender.FileCode,
					Type = TypeMassage.Error,
					LinesMassage = new(){
						Info.FullName + " FIleCode.CloseReading()",
						e.ToString()
					}
				});
			}
			finally { if (fs != null) fs.Dispose(); }
		}
	}
	public class LexerOut
	{
		public bool EndToken { get; set; }
		public bool NextToken { get; set; }
		public bool NextTokenLong { get; set; }
		public bool PrewToken { get; set; }
		public bool NewU { get; set; }
		public byte CountBytes { get; set; }
		public LexerOut()
		{
			EndToken = false;
			NextToken = false;
			NextTokenLong = false;
			PrewToken = false;
			NewU = false;
			CountBytes = 0;
		}
	}
	public class PosPoint
	{
		public long Value { private set; get; }
		public PosPoint(long v)
		{
			Value = v;
		}
	}
}