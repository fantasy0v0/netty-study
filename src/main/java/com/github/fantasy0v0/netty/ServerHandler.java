package com.github.fantasy0v0.netty;

import com.github.fantasy0v0.netty.vo.ServiceMessage;
import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.SimpleChannelInboundHandler;

import java.nio.charset.StandardCharsets;

public class ServerHandler extends SimpleChannelInboundHandler<ServiceMessage> {

  @Override
  protected void channelRead0(ChannelHandlerContext ctx, ServiceMessage data) throws Exception {
    ByteBuf msg = data.getData();
    short length = msg.readShort();
    byte[] businessData = new byte[length];
    msg.readBytes(businessData);
    String value = new String(businessData, StandardCharsets.UTF_8);
    System.out.println("业务数据: " + value);
    msg.release();
  }

}
