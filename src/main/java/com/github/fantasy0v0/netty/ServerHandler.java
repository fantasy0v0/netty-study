package com.github.fantasy0v0.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;

import java.nio.charset.StandardCharsets;

public class ServerHandler extends SimpleChannelInboundHandler<ByteBuf> {

  @Override
  protected void channelRead0(ChannelHandlerContext ctx, ByteBuf byteBuf) throws Exception {
    int readableBytes = byteBuf.readableBytes();
    System.out.println("可读的总长度: " + readableBytes);
    short length = byteBuf.readShort();
    System.out.println("需要读取的length: " + length);
    byte[] bytes = new byte[length];
    byteBuf.readBytes(bytes);
    String value = new String(bytes, StandardCharsets.UTF_8);
    System.out.println("value: " + value);
  }

}
