package com.github.fantasy0v0.netty.client;

import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;

import java.nio.charset.StandardCharsets;

public class ClientHandler extends ChannelInboundHandlerAdapter {

  @Override
  public void channelActive(ChannelHandlerContext ctx) {
    String request = "Hello World";
    ByteBuf buffer = Unpooled.buffer();
    // 消息类型 0 请求
    buffer.writeByte(0);
    // 消息id
    buffer.writeShort(1);
    // 业务类型
    buffer.writeShort(100);
    byte[] requestBytes = request.getBytes(StandardCharsets.UTF_8);
    buffer.writeShort(requestBytes.length);
    buffer.writeBytes(requestBytes);

    ByteBuf data = Unpooled.buffer();
    data.writeShort(buffer.readableBytes());
    data.writeBytes(buffer);
    ctx.writeAndFlush(data).addListener(t -> ctx.close());
  }

}
